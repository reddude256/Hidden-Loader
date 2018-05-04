<?php

$login = $_POST['log'];
$password = $_POST['pas'];
if($login == 'admin' && $password == 'admin' && !empty($login) && !empty($password)) 
{ 
    $lines = file('db.php');
    foreach($lines as $single_line)
    echo $single_line . "<br />\n"; 
} 
else 
{
$URL="https://google.com";
header ("Location: $URL"); 

}
