<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");
	if(!$conn)
	{
		echo "error_loging to data base"; 
		return ;
	}

	$key = $_POST["key"]; 
	$user = $_POST["user"]; 
	$pass = $_POST["pass"]; 
	if($key == "paid")
	{
		$query = "INSERT INTO users WHERE user_name = '$user' AND password = '$pass'";
		$result = mysqli_query($conn , $query);
		while($row = $result->fetch_assoc())
		{ 
			echo $row['XML_price_data'];
			return; 
		}
	}	

	if($key == "time")
	{
		$query = "SELECT XML_time_data FROM users WHERE user_name = '$user' AND password = '$pass'";
		$result = mysqli_query($conn , $query);
		while($row = $result->fetch_assoc())
		{
			echo $row['XML_time_data'];
			return; 
		}
	}	

	if($key == "saved")
	{
		$query = "SELECT XML_saved_data FROM users WHERE user_name = '$user' AND password = '$pass'";
		$result = mysqli_query($conn , $query);
		while($row = $result->fetch_assoc())
		{
			echo $row['XML_saved_data'];
			return; 
		}
	}	
	echo "0";
	
?>