<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");

	if(!$conn)
	{
		echo "error_loging to data base"; 
		return ;
	} 
	$article_id = $_POST["article_id"];

	$query = "SELECT * FROM image where article_id = '$article_id'";
 
	$result = mysqli_query($conn , $query);
 
  
	 echo mysqli_num_rows($result);
  
?>