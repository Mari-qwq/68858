<?php
session_start();

$login = $_POST['login'] ?? '';
$password = $_POST['password'] ?? '';

if (empty($login) || empty($password)) {
    $_SESSION['login_error'] = 'Введите логин и пароль.';
    header('Location: login.php');
    exit();
}

// Подключение к БД
$user = 'u68858';
$pass = '5450968';

try {
    $db = new PDO('mysql:host=localhost;dbname=u68858', $user, $pass, [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION
    ]);

    $stmt = $db->prepare("SELECT user_id, password_hash FROM users WHERE login = ?");
    $stmt->execute([$login]);
    $user_data = $stmt->fetch(PDO::FETCH_ASSOC);

    if ($user_data && password_verify($password, $user_data['password_hash'])) {
        $_SESSION['user_id'] = $user_data['user_id'];
        header('Location: edit.php');
        exit();
    } else {
        $_SESSION['login_error'] = 'Неверный логин или пароль.';
        header('Location: login.php');
        exit();
    }

} catch (PDOException $e) {
    $_SESSION['login_error'] = 'Ошибка подключения к БД.';
    header('Location: login.php');
    exit();
}
