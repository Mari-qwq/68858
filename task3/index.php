<?php
header('Content-Type: text/html; charset=UTF-8');

$errors = [];
$values = [
    'fio' => '',
    'phone' => '',
    'email' => '',
    'year' => '',
    'gender' => '',
    'languages' => [],
    'bio' => '',
    'agreement' => ''
];

// Функция очистки данных
function sanitize($data) {
    return htmlspecialchars(stripslashes(trim($data)));
}

// Если форма отправлена
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    // Валидация ФИО
    $values['fio'] = sanitize($_POST['fio']);
    if (empty($values['fio'])) {
        $errors['fio'] = 'Заполните имя.';
    } elseif (!preg_match("/^[a-zA-Zа-яА-Я\s]+$/u", $values['fio'])) {
        $errors['fio'] = 'ФИО должно содержать только буквы и пробелы.';
    } elseif (strlen($values['fio']) > 150) {
        $errors['fio'] = 'ФИО не должно превышать 150 символов.';
    }

    // Валидация телефона
    $values['phone'] = sanitize($_POST['phone']);
    if (empty($values['phone']) || !preg_match("/^[0-9\+\-\(\)\s]+$/", $values['phone'])) {
        $errors['phone'] = 'Некорректный формат телефона.';
    }

    // Валидация email
    $values['email'] = sanitize($_POST['email']);
    if (empty($values['email']) || !filter_var($values['email'], FILTER_VALIDATE_EMAIL)) {
        $errors['email'] = 'Некорректный email.';
    }

    // Валидация даты рождения
    $values['year'] = sanitize($_POST['year']);
    if (empty($values['year']) || !preg_match('/^\d{4}-\d{2}-\d{2}$/', $values['year'])) {
        $errors['year'] = 'Введите корректную дату рождения.';
    }

    // Валидация пола
    $values['gender'] = $_POST['gender'] ?? '';
    if (empty($values['gender']) || !in_array($values['gender'], ['male', 'female'])) {
        $errors['gender'] = 'Выберите пол.';
    }

    // Валидация языков программирования
    $values['languages'] = $_POST['languages'] ?? [];
    if (empty($values['languages']) || !is_array($values['languages'])) {
        $errors['languages'] = 'Выберите хотя бы один язык программирования.';
    }

    // Валидация биографии
    $values['bio'] = sanitize($_POST['bio']);
    if (empty($values['bio'])) {
        $errors['bio'] = 'Заполните биографию.';
    }

    // Проверка согласия
    if (empty($_POST['agreement'])) {
        $errors['agreement'] = 'Необходимо согласиться с условиями.';
    } else {
        $values['agreement'] = 'checked';
    }

    // Если ошибок нет, сохраняем в БД
    if (empty($errors)) {
        $user = 'u68858'; 
        $pass = '5450968'; 
        try {
            $db = new PDO('mysql:host=localhost;dbname=u68858', $user, $pass, [
                PDO::ATTR_PERSISTENT => true,
                PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION
            ]);

            $stmt = $db->prepare("INSERT INTO users (fio, phone, email, year, gender, bio) VALUES (?, ?, ?, ?, ?, ?)");
            $stmt->execute([
                $values['fio'], $values['phone'], $values['email'], 
                $values['year'], $values['gender'], $values['bio']
            ]);

            $user_id = $db->lastInsertId();

            foreach ($_POST['languages'] as $language) {
                $stmt_lang = $db->prepare("SELECT lang_id FROM langs WHERE lang_name = ?");
                $stmt_lang->execute([$language]);
                $lang_result = $stmt_lang->fetch(PDO::FETCH_ASSOC);

                if ($lang_result) {
                    $lang_id = $lang_result['lang_id'];
                    $stmt_user_lang = $db->prepare("INSERT INTO users_languages (user_id, lang_id) VALUES (?, ?)");
                    $stmt_user_lang->execute([$user_id, $lang_id]);
                }
            }

            $success_message = "Данные успешно сохранены!";
        } catch (PDOException $e) {
            $errors['db'] = "Ошибка сохранения данных: " . $e->getMessage();
        }
    }
}

include('form.php');
?>
