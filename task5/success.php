<?php
header('Content-Type: text/html; charset=UTF-8');

$login = $_COOKIE['new_login'] ?? '';
$password = $_COOKIE['new_password'] ?? '';
// Очищаем куки после показа
setcookie('new_login', '', time() - 3600, '/');
setcookie('new_password', '', time() - 3600, '/');
?>

<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <title>Успешная регистрация</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: #eecaca;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh;}

            .container{
                border-radius: 5px;
                background: rgb(255, 236, 237);
                padding: 20px;
            }

        </style>
</head>
<body>

<div class="container">
    <h1>Регистрация прошла успешно!</h1>

    <?php if ($login && $password): ?>
        <p><strong>Ваш логин:</strong> <?php echo htmlspecialchars($login); ?></p>
        <p><strong>Ваш пароль:</strong> <?php echo htmlspecialchars($password); ?></p>
        <p>Пожалуйста, сохраните эти данные. Они понадобятся для входа.</p>
    <?php else: ?>
        <p>Логин и пароль не найдены. Возможно, вы уже обновили страницу или срок действия куки истёк.</p>
    <?php endif; ?>
</body>
</html>
