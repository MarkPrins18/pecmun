<?php
connect();
function connect() {
	global $conn;
	
	require_once('.env');
	global $env;

	// Database configuration
	$servername = "localhost";
	$username = "root";
	$password = "";
	$dbname = "csd_iv_levelgenerator_v1_1";

	// Create connection
	$conn = new mysqli($env['dbHost'], $env['dbUser'], $env['dbPass'], $env['dbName']);

	// Check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}

	// echo "Connected successfully";
}

?>