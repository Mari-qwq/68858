<?php
$user = 'u68858'; 
$password = '5450968'; 
try {
    $pdo = new PDO('mysql:host=localhost;dbname=u68858', $user, $password,
        [PDO::ATTR_PERSISTENT => true, PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]);
} catch (PDOException $e) {
    die("<p style='color:red;'>Ошибка подключения к базе данных: " . $e->getMessage() . "</p>");
}
?>
