;
function formatST_UDTOGetData(response) {
    if (response.ResultDataValue == null || response.ResultDataValue == "") { return [];}
    var d = $.parseJSON(response.ResultDataValue);
    if (d.list) {
        return d.list;
    }
    if (d.rows) {
        return d.rows;
    }
    return d;
}

function formatST_Response(response){
	var res;
	if(response.success){
		if(response.ResultDataValue != "" && response.ResultDataValue != null){
			var data = JSON.parse(response.ResultDataValue);
			res = {
			  "code": 0,
			  "msg": "",
			  "count": data.count,
			  "data": data.list
			};
		}else{
			res = {
			  "code": 0,
			  "msg": "",
			  "count": 0,
			  "data": []
			};
		}
	}else{
		res = {
		  "code": 0,
		  "msg": response.ErrorInfo
		};
	}
	return res;
}
