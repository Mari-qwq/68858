<?php
// index.php
header('Content-Type: text/html; charset=UTF-8');

function sanitize($data) {
    return htmlspecialchars(stripslashes(trim($data)));
}

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

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // FIO
    $values['fio'] = sanitize($_POST['fio']);
    if (empty($values['fio']) || !preg_match('/^[a-zA-Zа-яА-Я\s-]+$/u', $values['fio'])) {
        $errors['fio'] = 'ФИО может содержать только буквы, пробелы и дефис.';
    }

    // Phone
    $values['phone'] = sanitize($_POST['phone']);
    if (empty($values['phone']) || !preg_match('/^\+?[0-9\-\s\(\)]+$/', $values['phone'])) {
        $errors['phone'] = 'Телефон может содержать только цифры, пробелы, скобки и знак +.';
    }

    // Email
    $values['email'] = sanitize($_POST['email']);
    if (empty($values['email']) || !preg_match('/^[\w\.\-]+@[\w\-]+\.[a-z]{2,6}$/i', $values['email'])) {
        $errors['email'] = 'Неверный формат email.';
    }

    // Year
    $values['year'] = sanitize($_POST['year']);
    if (empty($values['year']) || !preg_match('/^\d{4}$/', $values['year'])) {
        $errors['year'] = 'Введите корректный год рождения (4 цифры).';
    }

    // Gender
    $values['gender'] = $_POST['gender'] ?? '';
    if (!in_array($values['gender'], ['male', 'female'])) {
        $errors['gender'] = 'Выберите пол.';
    }

    // Languages
    $values['languages'] = $_POST['languages'] ?? [];
    $valid_languages = ['english', 'german', 'french'];
    foreach ($values['languages'] as $lang) {
        if (!in_array($lang, $valid_languages)) {
            $errors['languages'] = 'Недопустимые значения в списке языков.';
            break;
        }
    }

    // Bio
    $values['bio'] = sanitize($_POST['bio']);
    if (!empty($values['bio']) && !preg_match('/^[^<>]{0,1024}$/u', $values['bio'])) {
        $errors['bio'] = 'Биография содержит недопустимые символы или слишком длинная.';
    }

    // Agreement
    $values['agreement'] = $_POST['agreement'] ?? '';
    if ($values['agreement'] !== 'on') {
        $errors['agreement'] = 'Вы должны согласиться с условиями.';
    }

    if (!empty($errors)) {
        // Сохраняем ошибки и значения в cookies до конца сессии
        setcookie('errors', json_encode($errors), 0, '/');
        setcookie('values', json_encode($values), 0, '/');
        header('Location: form.php');
        exit();
    } else {
        // Успешно: сохраняем значения в куки на год, удаляем ошибки
        setcookie('errors', '', time() - 3600, '/');
        foreach ($values as $key => $value) {
            if (is_array($value)) {
                setcookie("values[$key]", json_encode($value), time() + 365*24*60*60, '/');
            } else {
                setcookie("values[$key]", $value, time() + 365*24*60*60, '/');
            }
        }
        header('Location: form.php');
        exit();
    }
}
