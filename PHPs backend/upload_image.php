<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");
	if(!$conn)
	{
		echo "error_loging to data base"; 
		return ;
	}
	$article_image = $_POST["article_image"];
	$article_id = $_POST["article_id"];
	$image_index = $_POST["image_index"];

	$query = "INSERT INTO image (image, image_index, article_id ) VALUES ('$article_image', '$image_index' , '$article_id')";
 
	$result = mysqli_query($conn , $query);
	echo mysqli_error($conn);
  
?>