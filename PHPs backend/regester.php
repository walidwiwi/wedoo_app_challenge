<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");

	if(!$conn)
	{
		echo "error_loging to data base"; 
		return ;
	}
	$user = $_POST["user"];
	$pass = $_POST["pass"];
	 

	$query = "INSERT INTO users (user_name, password ) VALUES ('$user', '$pass' )";
 
	$result = mysqli_query($conn , $query);
	echo "1";
  
?>