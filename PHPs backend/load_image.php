<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");
	
	if(!$conn)
	{
		echo "error_loging to data base"; 
		return ;
	}
	$image_index = $_POST["image_index"]; 
	$article_id = $_POST["article_id"];

	$query = "SELECT * FROM image WHERE article_id = '$article_id' AND image_index = '$image_index'";
	
	$result = mysqli_query($conn , $query);
		echo mysqli_error($conn);
	while($row = $result->fetch_assoc())
	{
		echo $row['image'];
	}
	
?>