<?php
require('dev.php');
require_once 'Data/levelData.php';

// Initialize levelData object
$levelData = new LevelData();

// Fetch all levels from the database
$levels = $levelData->getAllLevels();

if(isset($_GET['levelId'])) {
    // Get the requested level's object
    $levelObj = $levelData->getLevel($_GET['levelId']);
    
    // Load the requested level's XML
    $xml = new SimpleXMLElement($levelObj['xmlBody']);
}
else {

    // Create the XML structure
    $xml = new SimpleXMLElement('<levels/>');

    foreach ($levels as $level) {
        // Get the level object
        $levelObj =$levelData->getLevel($level['levelId']);

        // Load the XML body of the level
        $levelXml = new SimpleXMLElement($levelObj['xmlBody']);

        // Extract the required information
        $levelName = (string) $levelXml['name'];
        $levelDescription = (string) $levelXml['description'];
        $levelAuthor = (string) $levelXml['author']; // If the author is not stored in the xmlBody, remove this line.

        // Add the level information to the XML structure
        $levelElement = $xml->addChild('level');
        $levelElement->addAttribute('id', $levelObj['levelId']);
        $levelElement->addAttribute('order', $levelObj['levelOrder']);
        $levelElement->addChild('name', $levelName);
        $levelElement->addChild('description', $levelDescription);
        $levelElement->addChild('author', $levelAuthor); // If the author is not stored in the xmlBody, remove this line.
    }
	
}

// Set the content-type header and output the XML structure
header('Content-Type: application/xml');
echo $xml->asXML();