<?php
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $to = "matheusdecarvalhosampaio@gmail.com";
    $from = filter_var($_POST['email'], FILTER_SANITIZE_EMAIL);
    $name = $_POST['name'];
    $subject = $_POST['subject'];
    $cmessage = $_POST['message'];

    $headers = "From: $from\r\n";
    $headers .= "Reply-To: $from\r\n";
    $headers .= "MIME-Version: 1.0\r\n";
    $headers .= "Content-Type: text/html; charset=ISO-8859-1\r\n";

    $subject = "Você tem uma nova solicitação.";

    $logo = 'null empty';
    $link = '#';

    $body = "<!DOCTYPE html><html lang='pt-br'><head><meta charset='UTF-8'><title>Teste</title></head><body>";
    $body .= "<table style='width: 100%;'>";
    $body .= "<thead style='text-align: center;'><tr><td style='border:none;' colspan='2'>";
    $body .= "<a href='{$link}'><img src='{$logo}' alt=''></a><br><br>";
    $body .= "</td></tr></thead><tbody><tr>";
    $body .= "<td style='border:none;'><strong>Name:</strong> {$name}</td>";
    $body .= "<td style='border:none;'><strong>Email:</strong> {$from}</td>";
    $body .= "</tr>";
    $body .= "<tr><td style='border:none;'><strong>Subject:</strong> {$subject}</td></tr>";
    $body .= "<tr><td></td></tr>";
    $body .= "<tr><td colspan='2' style='border:none;'>{$cmessage}</td></tr>";
    $body .= "</tbody></table>";
    $body .= "</body></html";

    $send = mail($to, $subject, $body, $headers);

    if ($send) {
        echo "E-mail enviado com sucesso!";
    } else {
        echo "Ocorreu um erro ao enviar o e-mail.";
    }
} else {
    echo "Requisição inválida.";
}
?>