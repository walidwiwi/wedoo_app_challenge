<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");

	$article_id = $_POST["article_id"];

	$query = "SELECT * FROM article WHERE id = '$article_id'";
	$result = mysqli_query($conn ,$query);

	while($row = $result->fetch_assoc())
	{
		echo $row['name']."|". $row['normal_price']."|". $row['price']."|";
	}

?>