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

	$query = "SELECT * FROM users WHERE user_name = '$user' AND password = '$pass'";
	
	$result = mysqli_query($conn , $query);
		//echo mysqli_error($conn);
	while($row = $result->fetch_assoc())
	{
		echo "1";
		return; 
	}
	echo "0";
	
?>