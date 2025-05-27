<?php
/**
 * Админ-панель с HTTP-авторизацией для управления результатами.
 */

// Запрашиваем авторизацию
if (empty($_SERVER['PHP_AUTH_USER']) || empty($_SERVER['PHP_AUTH_PW'])) {
    header('WWW-Authenticate: Basic realm="Admin Area"');
    header('HTTP/1.1 401 Unauthorized');
    echo '<h1>401 Требуется авторизация</h1>';
    exit();
}

$login = $_SERVER['PHP_AUTH_USER'];
$password = $_SERVER['PHP_AUTH_PW'];

// Подключение к базе данных
$user = 'u68858';
$pass = '5450968';

try {
    $db = new PDO('mysql:host=localhost;dbname=u68858', $user, $pass, [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION
    ]);

    // Проверяем логин и пароль администратора в таблице admin_users
    $stmt = $db->prepare("SELECT password_hash FROM admin_users WHERE login = ?");
    $stmt->execute([$login]);
    $admin = $stmt->fetch(PDO::FETCH_ASSOC);

    if (!$admin || !password_verify($password, $admin['password_hash'])) {
        header('WWW-Authenticate: Basic realm="Admin Area"');
        header('HTTP/1.1 401 Unauthorized');
        echo '<h1>401 Неверный логин или пароль</h1>';
        exit();
    }

    if ($_SERVER['REQUEST_METHOD'] == 'POST' && isset($_POST['delete_id'])) {
    $delete_id = intval($_POST['delete_id']);
    $stmt = $db->prepare("DELETE FROM users_languages WHERE user_id = ?");
    $stmt->execute([$delete_id]);
    $stmt = $db->prepare("DELETE FROM users WHERE user_id = ?");
    $stmt->execute([$delete_id]);

    // Перенаправляем обратно на admin.php после удаления
    header('Location: admin.php');
    exit;
}

    // Читаем все данные пользователей
    $stmt = $db->query("SELECT u.user_id, u.fio, u.phone, u.email, u.year, u.gender, u.bio, 
        GROUP_CONCAT(l.lang_name SEPARATOR ', ') AS languages
        FROM users u
        LEFT JOIN users_languages ul ON u.user_id = ul.user_id
        LEFT JOIN langs l ON ul.lang_id = l.lang_id
        GROUP BY u.user_id");

    $users = $stmt->fetchAll(PDO::FETCH_ASSOC);

} catch (PDOException $e) {
    echo "Ошибка подключения: " . $e->getMessage();
    exit();
}
?>

<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <title>Админ-панель</title>
    <style>
        table { border-collapse: collapse; width: 100%; margin-top: 20px; background: rgb(255, 255, 255); }
        th, td { border: 1px solid #aaa; padding: 8px; }
        th { background: #eee; }
        a { text-decoration: none; color: #007BFF; }
        a:hover { text-decoration: underline; }
    </style>
</head>
<style>
        body {
            font-family: Arial, sans-serif;
            background: #eecaca;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh;}

            .userstable{
                border-radius: 5px;
                background: rgb(255, 236, 237);
                padding: 20px;

            }

            .statistics {
  max-width: 400px;
  margin: 20px auto;
  padding: 20px;
 border-radius: 5px;
                background: rgb(255, 236, 237);
                padding: 20px;
}

.statistics h2 {
  margin-bottom: 15px;
  color: #333;
  font-size: 1.6em;
  text-align: center;
}



.statistics li {
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
  font-size: 1.1em;
  color: #444;
}

.statistics li strong {
  color: #2a4d69;
}

        </style>
<body>
    <div class="userstable">
    <h1>Данные пользователей</h1>
    <table>
        <tr>
            <th>ID</th>
            <th>ФИО</th>
            <th>Телефон</th>
            <th>Email</th>
            <th>Дата рождения</th>
            <th>Пол</th>
            <th>Биография</th>
            <th>Языки</th>
            <th>Действия</th>
        </tr>
        <?php foreach ($users as $user): ?>
        <tr>
            <td><?= htmlspecialchars($user['user_id']) ?></td>
            <td><?= htmlspecialchars($user['fio']) ?></td>
            <td><?= htmlspecialchars($user['phone']) ?></td>
            <td><?= htmlspecialchars($user['email']) ?></td>
            <td><?= htmlspecialchars($user['year']) ?></td>
            <td><?= htmlspecialchars($user['gender']) ?></td>
            <td><?= htmlspecialchars($user['bio']) ?></td>
            <td><?= htmlspecialchars($user['languages']) ?></td>
            <td>
            <button type="button" onclick="window.location.href='admin_edit.php?id=<?= $user['user_id'] ?>'">Редактировать</button>
        
             <form method="post" style="display:inline;">
                <input type="hidden" name="delete_id" value="<?= $user['user_id'] ?>" >
                 <button type="submit" onclick="return confirm('Удалить пользователя?')">Удалить</button>
                 </form>
            </td>
        </tr>
        <?php endforeach; ?>
    </table>
        </div>
        <div class="statistics">
    <h2>Статистика</h2>
    <?php
    // Статистика: сколько пользователей выбрали каждый язык
    $stmt = $db->query("SELECT l.lang_name, COUNT(ul.user_id) AS user_count
        FROM langs l
        LEFT JOIN users_languages ul ON l.lang_id = ul.lang_id
        GROUP BY l.lang_id");
    $stats = $stmt->fetchAll(PDO::FETCH_ASSOC);
    ?>
    <ul>
        <?php foreach ($stats as $stat): ?>
            <li><?= htmlspecialchars($stat['lang_name']) ?>: <?= $stat['user_count'] ?> пользователей</li>
        <?php endforeach; ?>
    </ul>
        </div>
</body>
</html>
