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

function sanitize($data) {
    return htmlspecialchars(stripslashes(trim($data)));
}

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
    if (!empty($_COOKIE['errors'])) {
        $errors = unserialize($_COOKIE['errors']);
        setcookie('errors', '', time() - 3600, '/');
    }

    if (!empty($_COOKIE['values'])) {
        $values = unserialize($_COOKIE['values']);
        setcookie('values', '', time() - 3600, '/');
    } else {
        foreach ($values as $key => $_) {
            if (!empty($_COOKIE['saved_' . $key])) {
                $values[$key] = in_array($key, ['languages']) 
                    ? unserialize($_COOKIE['saved_' . $key]) 
                    : $_COOKIE['saved_' . $key];
            }
        }
    }

    include('form.php');
    exit();
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $values['fio'] = sanitize($_POST['fio']);
    if (empty($values['fio'])) {
        $errors['fio'] = 'Заполните имя.';
    } elseif (!preg_match("/^[a-zA-Zа-яА-Я\s]+$/u", $values['fio'])) {
        $errors['fio'] = 'ФИО должно содержать только буквы и пробелы.';
    } elseif (strlen($values['fio']) > 150) {
        $errors['fio'] = 'ФИО не должно превышать 150 символов.';
    }

    $values['phone'] = sanitize($_POST['phone']);
    if (empty($values['phone']) ){
        $errors['phone'] = 'Введите номер.';
    }elseif( !preg_match("/^[0-9\+\-\(\)\s]+$/", $values['phone'])){
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

    if (!empty($errors)) {
        setcookie('errors', serialize($errors), 0, '/');
        setcookie('values', serialize($values), 0, '/');
        header('Location: index.php');
        exit();
    }

    // Подключение и запись в БД
    $user = 'u68858';
    $pass = '5450968';
    try {
        $db = new PDO('mysql:host=localhost;dbname=u68858', $user, $pass, [
            PDO::ATTR_PERSISTENT => true,
            PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION
        ]);

        $login = uniqid('user_');
        $password = bin2hex(random_bytes(4)); // 8 случайных символов
        $hashed_password = password_hash($password, PASSWORD_DEFAULT);
        


        $stmt = $db->prepare("INSERT INTO users (fio, phone, email, year, gender, bio, login, password_hash)
                      VALUES (?, ?, ?, ?, ?, ?, ?, ?)");
$stmt->execute([
    $values['fio'], $values['phone'], $values['email'], 
    $values['year'], $values['gender'], $values['bio'],
    $login, $hashed_password
]);
        $user_id = $db->lastInsertId();

        foreach ($values['languages'] as $language) {
            $stmt_lang = $db->prepare("SELECT lang_id FROM langs WHERE lang_name = ?");
            $stmt_lang->execute([$language]);
            $lang_result = $stmt_lang->fetch(PDO::FETCH_ASSOC);
            if ($lang_result) {
                $stmt_user_lang = $db->prepare("INSERT INTO users_languages (user_id, lang_id) VALUES (?, ?)");
                $stmt_user_lang->execute([$user_id, $lang_result['lang_id']]);
            }
        }

        foreach ($values as $key => $val) {
            setcookie('saved_' . $key, is_array($val) ? serialize($val) : $val, time() + 365*24*60*60, '/');
        }
        
        setcookie('new_login', $login, time() + 60, '/');
        setcookie('new_password', $password, time() + 60, '/');
        
} catch (PDOException $e) {
        setcookie('errors', serialize(['db' => $e->getMessage()]), 0, '/');
    }
        
        header('Location: success.php');
    exit();
    
    include 'form.php';
}


?>

