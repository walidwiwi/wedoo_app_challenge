<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
	if(!$conn)
	{
		echo "error_loging to data base"; 
		return ;
	} 
	$cat_id = $_POST["cat_id"];
	$lim = $_POST["num"];

	$query = "SELECT article_id FROM article_cat where article_cat_id = '$cat_id'";
 
	$result = mysqli_query($conn , $query);
 	

    $i = 0;
	while($row = $result->fetch_assoc())
	{ 
		if($i == $lim)
				break;
		echo $row['article_id']."|" ;
		$i++; 
	}
  
?>