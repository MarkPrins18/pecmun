<?php 

/**
 * @description Function prints array
 **/
function vA($a_array, $b_return=false, $b_newCell=false, $i_counter=0)	//To print an (multiple)array in a table
{
	$s_retVal = '';

	$i_counter++;
	if(!is_array($a_array))
		$s_retVal .= 'Geen array: "'.$a_array.'".<br />';
	elseif( empty($a_array) && $i_counter == 0 )
	{
		$s_retVal .= 'Lege array op niveau '.$i_counter.'<br />';
	}
	elseif( $a_array=="" )
		$s_retVal .= 'Vreemde fout: "'.$a_array.'".<br />';
	else
	{
		if($b_newCell)
			$s_retVal .= '<td>';

		$s_retVal .= '<table border="'.$i_counter.'" >';
		foreach( $a_array as $s_key => $s_value )
		//while(list($s_key, $s_value) = each($a_array))
		{
			$s_retVal .= '<tr><td style="vertical-align:top;"><b>'.$s_key.'</b></td>';
			if (is_array($s_value))
				$s_retVal .= vA($s_value, true, true, $i_counter, false); 
			else{
				if(is_object($s_value))
				{
					/* $a_backtrace = debug_backtrace();
					$s_retVal .= '<td>Object in vA():<strong>'.$a_backtrace[0]['line'].' - '. substr( $a_backtrace[0]['file'], strrpos($a_backtrace[0]['file'],'/')+1).'</strong></td>'; */

					// Temporary, because debug backtrace fails objects
					$s_retVal .= '<td><b>OBJECT BACKTRACE</b></td>'; 
				}
				else
					$s_retVal .= '<td><b>'.$s_value.'</b></td>'; 
			}

			$s_retVal .= '</tr>';
		}
		$s_retVal .= '</table>';
		if($b_newCell)
			$s_retVal .= '</td>';
	}

	if ( isset ( $s_title ) )
	{
		$s_retVal = '<h2>'.$s_title.'</h2>'.$s_retVal;
	}

	if($b_return)
		return $s_retVal;
	else
		echo $s_retVal;
}