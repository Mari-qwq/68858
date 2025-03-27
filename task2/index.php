<?php
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $name = htmlspecialchars($_POST['name']);
    $comment = htmlspecialchars($_POST['comment']);

    if (!empty($name) && !empty($comment)) {
        // Сохранение в файл (можно заменить на запись в БД)
        $file = fopen("comments.txt", "a");
        fwrite($file, "Имя: $name\nКомментарий: $comment\n---\n");
        fclose($file);

        echo "Комментарий успешно отправлен!";
    } else {
        echo "Ошибка: заполните все поля.";
    }
} else {
    echo "Используйте метод POST для отправки комментария.";
}
?>
