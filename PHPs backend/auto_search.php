<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");

	$key_string_fragma = $_POST['key_string_fragma'];

	$query = "SELECT * FROM keys_avariable WHERE key_string like '$key_string_fragma'";
	$result = mysqli_query($conn, $query);

	//echo mysqli_error($conn);
	while($row = $result->fetch_assoc())
	{
		echo $row['key_string']."|";
	}


?>