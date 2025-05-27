<?php
// Подключаемся к БД
function sanitize($val) {
    return htmlspecialchars(trim($val));
}
$user = 'u68858';
$pass = '5450968';

try {
    $db = new PDO('mysql:host=localhost;dbname=u68858', $user, $pass, [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION
    ]);

    // Проверка HTTP-авторизации
    if (empty($_SERVER['PHP_AUTH_USER']) || empty($_SERVER['PHP_AUTH_PW'])) {
        header('HTTP/1.1 401 Unauthorized');
        header('WWW-Authenticate: Basic realm="My site"');
        echo 'Требуется авторизация';
        exit;
    }

    $login = $_SERVER['PHP_AUTH_USER'];
    $password = $_SERVER['PHP_AUTH_PW'];

    $stmt = $db->prepare("SELECT password_hash FROM admin_users WHERE login = ?");
    $stmt->execute([$login]);
    $admin = $stmt->fetch(PDO::FETCH_ASSOC);

    if (!$admin || !password_verify($password, $admin['password_hash'])) {
        header('HTTP/1.1 403 Forbidden');
        echo 'Неверные данные';
        exit;
    }

    // Получаем ID пользователя из GET
    $user_id = isset($_GET['id']) ? intval($_GET['id']) : 0;
    if ($user_id <= 0) {
        echo "Некорректный ID.";
        exit();
    }

    $errors = [];
    $values = [];

    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        // Валидация
        $values['fio'] = sanitize($_POST['fio']);
        if (empty($values['fio'])) {
            $errors['fio'] = 'Заполните имя.';
        } elseif (!preg_match("/^[a-zA-Zа-яА-Я\s]+$/u", $values['fio'])) {
            $errors['fio'] = 'ФИО должно содержать только буквы и пробелы.';
        } elseif (strlen($values['fio']) > 150) {
            $errors['fio'] = 'ФИО не должно превышать 150 символов.';
        }

        $values['phone'] = sanitize($_POST['phone']);
        if (empty($values['phone'])) {
            $errors['phone'] = 'Введите номер.';
        } elseif (!preg_match("/^[0-9\+\-\(\)\s]+$/", $values['phone'])) {
            $errors['phone'] = 'Номер телефона может содержать только цифры 0-9, +, -, (, ).';
        }

        $values['email'] = sanitize($_POST['email']);
        if (empty($values['email']) || !filter_var($values['email'], FILTER_VALIDATE_EMAIL)) {
            $errors['email'] = 'Некорректный email.';
        }

        $values['year'] = sanitize($_POST['year']);
        if (empty($values['year']) || !preg_match('/^\d{4}-\d{2}-\d{2}$/', $values['year'])) {
            $errors['year'] = 'Введите корректную дату рождения.';
        }

        $values['gender'] = $_POST['gender'] ?? '';
        if (empty($values['gender']) || !in_array($values['gender'], ['male', 'female'])) {
            $errors['gender'] = 'Выберите пол.';
        }

        $values['languages'] = $_POST['languages'] ?? [];
        if (empty($values['languages']) || !is_array($values['languages'])) {
            $errors['languages'] = 'Выберите хотя бы один язык программирования.';
        }

        $values['bio'] = sanitize($_POST['bio']);
        if (empty($values['bio'])) {
            $errors['bio'] = 'Заполните биографию.';
        }

        if (empty($_POST['agreement'])) {
            $errors['agreement'] = 'Необходимо согласиться с условиями.';
        } else {
            $values['agreement'] = 'checked';
        }

        if (empty($errors)) {
            // Обновление данных пользователя
            $stmt = $db->prepare("UPDATE users SET fio=?, phone=?, email=?, year=?, gender=?, bio=? WHERE user_id=?");
            $stmt->execute([
                $values['fio'], $values['phone'], $values['email'],
                $values['year'], $values['gender'], $values['bio'], $user_id
            ]);

            // Удаление старых связей
            $stmt = $db->prepare("DELETE FROM users_languages WHERE user_id=?");
            $stmt->execute([$user_id]);

            // Добавление новых языков
            foreach ($values['languages'] as $lang) {
                $stmt_lang = $db->prepare("SELECT lang_id FROM langs WHERE lang_name=?");
                $stmt_lang->execute([$lang]);
                $lang_result = $stmt_lang->fetch(PDO::FETCH_ASSOC);
                if ($lang_result) {
                    $stmt_ul = $db->prepare("INSERT INTO users_languages (user_id, lang_id) VALUES (?, ?)");
                    $stmt_ul->execute([$user_id, $lang_result['lang_id']]);
                }
            }

            $success = true;
        }
    }

    // Получаем текущие данные пользователя
    $stmt = $db->prepare("SELECT * FROM users WHERE user_id=?");
    $stmt->execute([$user_id]);
    $user_data = $stmt->fetch(PDO::FETCH_ASSOC);

    if (!$user_data) {
        echo "Пользователь не найден.";
        exit();
    }

    // Если форма не отправлена, заполняем $values из БД
    if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
        $values = $user_data;

        // Получаем языки
        $stmt = $db->prepare("SELECT lang_name FROM langs 
            JOIN users_languages USING (lang_id) WHERE user_id = ?");
        $stmt->execute([$user_id]);
        $values['languages'] = $stmt->fetchAll(PDO::FETCH_COLUMN);
    }

} catch (PDOException $e) {
    echo "Ошибка БД: " . $e->getMessage();
    exit();
}
?>

<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Редактирование профиля</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
<h1>Редактирование профиля</h1>

<?php if (!empty($errors)): ?>
    <ul style="color:red;">
        <?php foreach ($errors as $msg): ?>
            <li><?= $msg ?></li>
        <?php endforeach; ?>
    </ul>
<?php elseif (!empty($success)): ?>
    <p style="color:green;">Данные обновлены успешно!</p>
<?php endif; ?>

<form method="POST">
    <label>ФИО:
        <input name="fio" value="<?= htmlspecialchars($values['fio'] ?? '') ?>">
    </label>

    <label>Телефон:
        <input name="phone" value="<?= htmlspecialchars($values['phone'] ?? '') ?>">
    </label>

    <label>Email:
        <input name="email" value="<?= htmlspecialchars($values['email'] ?? '') ?>">
    </label>

    <label>Дата рождения:
        <input type="date" name="year" value="<?= htmlspecialchars($values['year'] ?? '') ?>">
    </label>

    <label>Пол:
        <input type="radio" name="gender" value="male" <?= ($values['gender'] ?? '') === 'male' ? 'checked' : '' ?>> Мужской
        <input type="radio" name="gender" value="female" <?= ($values['gender'] ?? '') === 'female' ? 'checked' : '' ?>> Женский
    </label>

    <label>Языки программирования:</label>
<select name="languages[]" multiple>
    <?php
    $all_languages = ['Pascal', 'C', 'C++', 'JavaScript', 'PHP', 'Python', 'Java', 'Haskell', 'Clojure', 'Prolog', 'Scala', 'Go'];
    foreach ($all_languages as $lang) {
        $selected = in_array($lang, $values['languages'] ?? []) ? 'selected' : '';
        echo "<option value=\"$lang\" $selected>$lang</option>";
    }
    ?>
</select>

    <label>Биография:
        <textarea name="bio"><?= htmlspecialchars($values['bio'] ?? '') ?></textarea>
    </label>

    <label>
        <input type="checkbox" name="agreement" <?= !empty($values['agreement']) ? 'checked' : '' ?>> Согласие с условиями
    </label>

    <input type="submit" value="Сохранить">
</form>
<p><a href="admin.php"> Назад к списку пользователей</a></p>
</body>
</html>
