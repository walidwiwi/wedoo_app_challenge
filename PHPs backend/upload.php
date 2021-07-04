<?php 
	$conn = mysqli_connect("localhost" , "290327" , "walid123");
	mysqli_select_db($conn , "290327");

	if(!$conn)
	{
		echo "error_loging to data base"; 
		return ;
	}
	$article_name = $_POST["article_name"];
	$article_price = $_POST["article_price"];
	$article_super_price = $_POST["article_super_price"];
	$article_normal_price = $_POST["article_normal_price"]; 
	$article_qnt = $_POST["article_qnt"];
	$article_gener = $_POST["article_gener"];
	$article_keys = $_POST["article_keys"];
	$stock_id = $_POST['stock_id'];
	$disc = $_POST['disc'];

	$query = "INSERT INTO article (name,nb_vue,normal_price,price,super_price,qnt,gener,ID_stock,remis)VALUES('$article_name','0','$article_normal_price','$article_price','$article_super_price','$article_qnt','$article_gener','$stock_id','0%' , '$disc')";

	$last_id_insred = -1;
	if($conn->query($query) === TRUE)
	{
		$last_id_insred = $conn->insert_id;
	}else
	{
		echo mysqli_error($conn);
		die();
	}
	echo $last_id_insred;
	$art_keys = explode(" ", $article_keys);
	foreach ($art_keys as $value) {
		$query = "SELECT * FROM keys_avariable WHERE key_string = '$value'";
		$result = mysqli_query($conn ,$query);
		if(mysqli_num_rows($result) == 0)
		{

			$query = "INSERT INTO keys_avariable (key_string) VALUES ('$value')";
			$result = mysqli_query($conn ,$query);
		}

		$query = "SELECT id FROM keys_avariable WHERE key_string = '$value'";
		$result = mysqli_query($conn ,$query);

		$key_id = -1;

		while ($row = $result->fetch_assoc()) {
			$key_id = $row['id'];
		}
		if($key_id != -1)
		{
			$query = "INSERT INTO art_key (art_id, key_id) VALUES ($last_id_insred , $key_id )";
			$result = mysqli_query($conn , $query);
			echo mysqli_error($conn);
		}
		else 
			echo "err in key load ids";
	}



?>