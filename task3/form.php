<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Задание 3</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="style.css">
</head>
  <body>
  
<form id = "form" action="index.php" method="POST">
<label for="fio">ФИО:</label>
    <input type="text" id="fio" name="fio" value="<?php echo htmlspecialchars($values['fio']); ?>">
    <span class="error"><?php echo $errors['fio'] ?? ''; ?></span>

    <label for="phone">Телефон:</label>
    <input type="tel" id="phone" name="phone" value="<?php echo htmlspecialchars($values['phone']); ?>">
    <span class="error"><?php echo $errors['phone'] ?? ''; ?></span>

    <label for="email">E-mail:</label>
    <input type="email" id="email" name="email" value="<?php echo htmlspecialchars($values['email']); ?>">
    <span class="error"><?php echo $errors['email'] ?? ''; ?></span>

    <label for="year">Дата рождения:</label>
    <input type="date" id="year" name="year" value="<?php echo htmlspecialchars($values['year']); ?>">
    <span class="error"><?php echo $errors['year'] ?? ''; ?></span>

    <label>Пол:</label>
    <input type="radio" name="gender" value="male" <?php echo $values['gender'] === 'male' ? 'checked' : ''; ?>> Мужской
    <input type="radio" name="gender" value="female" <?php echo $values['gender'] === 'female' ? 'checked' : ''; ?>> Женский
    <span class="error"><?php echo $errors['gender'] ?? ''; ?></span>

    <label>Языки программирования:</label>
    <select name="languages[]" multiple>
    <option value="Pascal">Pascal</option>
                    <option value="C">C</option>
                    <option value="C++">C++</option>
                    <option value="JavaScript">JavaScript</option>
                    <option value="PHP">PHP</option>
                    <option value="Python">Python</option>
                    <option value="Java">Java</option>
                    <option value="Haskell">Haskell</option>
                    <option value="Clojure">Clojure</option>
                    <option value="Prolog">Prolog</option>
                    <option value="Scala">Scala</option>
                    <option value="Go">Go</option>
                </select>
    <span class="error"><?php echo $errors['languages'] ?? ''; ?></span>

    <label for="bio">Биография:</label>
    <textarea id="bio" name="bio"><?php echo htmlspecialchars($values['bio']); ?></textarea>
    <span class="error"><?php echo $errors['bio'] ?? ''; ?></span>

    <input type="checkbox" id="agreement" name="agreement" <?php echo $values['agreement']; ?>>
    <label for="agreement">Согласен с условиями</label>
    <span class="error"><?php echo $errors['agreement'] ?? ''; ?></span>

    <button type="submit">Сохранить</button>
              </form>

              <?php if (!empty($success_message)): ?>
    <p style="color:green;"><?php echo $success_message; ?></p>
<?php endif; ?>

<?php if (!empty($errors['db'])): ?>
    <p style="color:red;"><?php echo $errors['db']; ?></p>
<?php endif; ?>
  </body>
