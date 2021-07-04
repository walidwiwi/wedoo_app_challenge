<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");
	if(!$conn)
	{
		echo "error_loging to data base"; 
		return ;
	}

	$key  = $_POST["key"]; 
	$data = $_POST["data"];  
	$pass = $_POST["pass"];
	$user = $_POST["user"];
	if($key == "paid")
	{ 
		$query = "UPDATE users SET XML_price_data='$data' WHERE user_name = '$user' AND password = '$pass'";
		$result = mysqli_query($conn , $query);
 	}	

	if($key == "time")
	{
		$query = "UPDATE users SET XML_time_data='$data' WHERE user_name = '$user' AND password = '$pass'";
		$result = mysqli_query($conn , $query);
	}	

	if($key == "saved")
	{
		$query = "UPDATE users SET XML_saved_data='$data' WHERE user_name = '$user' AND password = '$pass'";
 	}	 
	
?>

