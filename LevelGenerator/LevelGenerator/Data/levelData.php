<?php

class LevelData {
    private $conn;

    public function __construct() {
        $this->connect();
    }
	

    private function connect() {
        		global $env;

		require_once('.env');
		
        $this->conn = new mysqli($env['dbHost'], $env['dbUser'], $env['dbPass'], $env['dbName']);

        if ($this->conn->connect_error) {
            die("Connection failed: " . $this->conn->connect_error);
        }
    }
	
public function getAllLevels() {
    $query = "SELECT levelId, xmlBody, levelOrder FROM levels ORDER BY levelOrder";
    $result = $this->conn->query($query);
	if($result === false) {
		die("Error: " . $this->conn->error);
    }
    $levels = [];

	// check if the query returned any results
	if ($result->num_rows > 0) {
		while ($row = $result->fetch_assoc()) {
			$levels[] = $row;
		}
	}

    return $levels;
}

public function getLevel($levelId) {
    $query = "SELECT levelId, xmlBody, levelOrder FROM levels WHERE levelId = ?";
    $stmt = $this->conn->prepare($query);
    $stmt->bind_param('i', $levelId);
    $stmt->execute();
    $result = $stmt->get_result();
    $level = $result->fetch_assoc();

    return $level;
}


public function saveLevel($levelId, $xmlBody) {
    if ($levelId == -1) {
        // Create new level
        $query = "SELECT MAX(levelOrder) as maxLevelOrder FROM levels";
        $result = $this->conn->query($query);
        $row = $result->fetch_assoc();
        $newLevelOrder = $row['maxLevelOrder'] + 1;

        $query = "INSERT INTO levels (xmlBody, levelOrder) VALUES (?, ?)";
        $stmt = $this->conn->prepare($query);
        $stmt->bind_param('si', $xmlBody, $newLevelOrder);
    } else {
        // Update existing level
        $query = "UPDATE levels SET xmlBody = ? WHERE levelId = ?";
        $stmt = $this->conn->prepare($query);
        $stmt->bind_param('si', $xmlBody, $levelId);
    }

    $stmt->execute();
}


    public function deleteLevel($levelId) {
        $query = "DELETE FROM levels WHERE levelId = ?";
        $stmt = $this->conn->prepare($query);
        $stmt->bind_param('i', $levelId);
        $stmt->execute();
    }

public function swapLevelOrder($levelId, $direction) {
    $currentLevel = $this->getLevel($levelId);
    
    if ($direction === 'up') {
        $query = "SELECT * FROM levels WHERE levelOrder < ? ORDER BY levelOrder DESC LIMIT 1";
    } else {
        $query = "SELECT * FROM levels WHERE levelOrder > ? ORDER BY levelOrder ASC LIMIT 1";
    }
    
    $stmt = $this->conn->prepare($query);
    $stmt->bind_param('i', $currentLevel['levelOrder']);
    $stmt->execute();
    $result = $stmt->get_result();
    $otherLevel = $result->fetch_assoc();

    if ($otherLevel) {
        $query = "UPDATE levels SET levelOrder = ? WHERE levelId = ?";
        $stmt = $this->conn->prepare($query);

        $stmt->bind_param('ii', $otherLevel['levelOrder'], $currentLevel['levelId']);
        $stmt->execute();

        $stmt->bind_param('ii', $currentLevel['levelOrder'], $otherLevel['levelId']);
        $stmt->execute();
    }
}
// A method to create a new, empty level, with a max levelOrder, and return it's levelId
public function createNewLevel() {
	$query = "SELECT MAX(levelOrder) as maxLevelOrder FROM levels";
	$result = $this->conn->query($query);
	$row = $result->fetch_assoc();
	$newLevelOrder = $row['maxLevelOrder'] + 1;

	$query = "INSERT INTO levels (xmlBody, levelOrder) VALUES ('', ?)";
	$stmt = $this->conn->prepare($query);
	$stmt->bind_param('i', $newLevelOrder);
	$stmt->execute();

	return $stmt->insert_id;
}
}