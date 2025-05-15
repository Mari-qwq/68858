<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Задание 3</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
<?php
if (!isset($values)) {
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
}

if (!isset($errors)) {
    $errors = [];
}

if (!isset($success_message)) {
    $success_message = '';
}
?>

<?php if (!empty($success_message)): ?>
    <p class="success-message"><?php echo $success_message; ?></p>
<?php endif; ?>

<?php if (!empty($errors['db'])): ?>
    <p class="error-message"><?php echo $errors['db']; ?></p>
<?php endif; ?>

<form id="form" action="index.php" method="POST">

    <!-- ФИО -->
    <label for="fio">ФИО:</label>
    <input type="text" id="fio" name="fio" value="<?php echo htmlspecialchars($values['fio']); ?>"
           class="<?php echo isset($errors['fio']) ? 'error-field' : ''; ?>">
    <span class="error"><?php echo $errors['fio'] ?? ''; ?></span>

    <!-- Телефон -->
    <label for="phone">Телефон:</label>
    <input type="tel" id="phone" name="phone" value="<?php echo htmlspecialchars($values['phone']); ?>"
           class="<?php echo isset($errors['phone']) ? 'error-field' : ''; ?>">
    <span class="error"><?php echo $errors['phone'] ?? ''; ?></span>

    <!-- Email -->
    <label for="email">E-mail:</label>
    <input type="email" id="email" name="email" value="<?php echo htmlspecialchars($values['email']); ?>"
           class="<?php echo isset($errors['email']) ? 'error-field' : ''; ?>">
    <span class="error"><?php echo $errors['email'] ?? ''; ?></span>

    <!-- Дата рождения -->
    <label for="year">Дата рождения:</label>
    <input type="date" id="year" name="year" value="<?php echo htmlspecialchars($values['year']); ?>"
           class="<?php echo isset($errors['year']) ? 'error-field' : ''; ?>">
    <span class="error"><?php echo $errors['year'] ?? ''; ?></span>

    <!-- Пол -->
    <label>Пол:</label>
    <input type="radio" name="gender" value="male" <?php echo $values['gender'] === 'male' ? 'checked' : ''; ?>> Мужской
    <input type="radio" name="gender" value="female" <?php echo $values['gender'] === 'female' ? 'checked' : ''; ?>> Женский
    <span class="error"><?php echo $errors['gender'] ?? ''; ?></span>

    <!-- Языки -->
    <label>Языки программирования:</label>
    <select name="languages[]" multiple>
        <?php
        $all_languages = ['Pascal', 'C', 'C++', 'JavaScript', 'PHP', 'Python', 'Java', 'Haskell', 'Clojure', 'Prolog', 'Scala', 'Go'];
        foreach ($all_languages as $lang) {
            $selected = in_array($lang, $values['languages']) ? 'selected' : '';
            echo "<option value=\"$lang\" $selected>$lang</option>";
        }
        ?>
    </select>
    <span class="error"><?php echo $errors['languages'] ?? ''; ?></span>

    <!-- Биография -->
    <label for="bio">Биография:</label>
    <textarea id="bio" name="bio" class="<?php echo isset($errors['bio']) ? 'error-field' : ''; ?>"><?php echo htmlspecialchars($values['bio']); ?></textarea>
    <span class="error"><?php echo $errors['bio'] ?? ''; ?></span>

    <!-- Согласие -->
    <input type="checkbox" id="agreement" name="agreement" <?php echo $values['agreement'] === 'checked' ? 'checked' : ''; ?>>
    <label for="agreement">Согласен с условиями</label>
    <span class="error"><?php echo $errors['agreement'] ?? ''; ?></span>

    <br><br>
    <button type="submit">Сохранить</button>
</form>

</body>
</html>
