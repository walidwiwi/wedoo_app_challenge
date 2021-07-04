<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");
	
	$key_ = $_POST['key'];

	$query = "SELECT * FROM keys_avariable WHERE key_string = '$key_'";
	$result = mysqli_query($conn, $query);

	$key_target_id = 0;
	//echo mysqli_error($conn);
	while($row = $result->fetch_assoc())
	{
		$key_target_id = $row['id'];
	}

	$query = "SELECT * FROM art_key WHERE key_id = '$key_target_id'";
	$result = mysqli_query($conn, $query);
	

	while($row = $result->fetch_assoc())
	{
		echo $row['art_id']."|";
	}

?>