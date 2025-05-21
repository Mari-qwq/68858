<?php
session_start();
?>

<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <title>Вход</title>
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

            .container{
                border-radius: 5px;
                background: rgb(255, 236, 237);
                padding: 20px;
            }

        </style>
<body>
<div class="container">
    <h1>Вход</h1>

    <?php if (!empty($_SESSION['login_error'])): ?>
        <p style="color: red;"><?php echo $_SESSION['login_error']; unset($_SESSION['login_error']); ?></p>
    <?php endif; ?>

    <form method="POST" action="login_verification.php">
        <label>Логин:<br>
            <input type="text" name="login" required>
        </label><br><br>

        <label>Пароль:<br>
            <input type="password" name="password" required>
        </label><br><br>

        <input type="submit" value="Войти">
    </form>
    </div>
</body>
</html>
