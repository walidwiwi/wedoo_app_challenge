<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");


	$query = "SELECT * FROM cat";
	$result = mysqli_query($conn ,$query);

	while($row = $result->fetch_assoc())
	{
		echo $row['gat_name']."|" ;
	}

?>