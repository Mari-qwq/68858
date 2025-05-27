INSERT INTO admin_users (login, password_hash)
VALUES ('admin', '$2y$10$RPTDZSiiLpT4CFyslfqea.skca8SceDfYHMWhVv5Z9Z6NKKNTL6Gm');

<?php
echo password_hash('adminpass', PASSWORD_DEFAULT);
?>
