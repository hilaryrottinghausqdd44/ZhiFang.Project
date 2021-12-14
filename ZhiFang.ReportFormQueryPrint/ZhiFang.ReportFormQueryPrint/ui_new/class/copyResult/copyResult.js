 /* @author ghx
 * @version 2019-07-26
 */
	//结果复选框全选和取消全选
	function allcheck() {
     var nn = $("#allboxs").is(":checked"); //判断th中的checkbox是否被选中，如果被选中则nn为true，反之为false
         if(nn == true) {
             var namebox = $("input[name^='boxs']");  //获取name值为boxs的所有input
             for(i = 0; i < namebox.length; i++) {
                 namebox[i].checked=true;    //js操作选中checkbox
             }
         }
         if(nn == false) {
             var namebox = $("input[name^='boxs']");
             for(i = 0; i < namebox.length; i++) {
                 namebox[i].checked=false;
             }
         }
    };
	
	//复制普通生化XSlt模板
	function copyNormal() {
        var namebox = $("input[name^='boxs']");  //获取name值为boxs的所有input
        //var results = [];
		var results = "";  
		//theResultYouWant--模板里配置的隐藏项，value值为想要复制的列，用逗号隔开
		var theResultsYouWant=document.getElementById ('theResultYouWant') ;
		if(theResultsYouWant && theResultsYouWant.value){
			var wantResultsColumn = theResultsYouWant.value;
			var resultsColumnList = wantResultsColumn.split(",");
			for(i = 0; i < namebox.length; i++) {
				//namebox[i].checked=true; //js操作选中checkbox
				if( namebox[i].checked == true){
					var row = namebox[i].getAttribute("rownum");
					for(j = 0; j < resultsColumnList.length; j++){
						results += document.getElementById ('normalxsl').rows[row].cells[resultsColumnList[j]].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +"  ";
					}
					results +=";"+"\r\n";
				}
			}
		}else{
			for(i = 0; i < namebox.length; i++) {
				//namebox[i].checked=true; //js操作选中checkbox
				if( namebox[i].checked == true){
					var row = namebox[i].getAttribute("rownum");
					results += row +"  "+
									document.getElementById ('normalxsl').rows[row].cells[2].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +"  "+
									document.getElementById ('normalxsl').rows[row].cells[3].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +"  "+
									document.getElementById ('normalxsl').rows[row].cells[4].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +"  "+
									document.getElementById ('normalxsl').rows[row].cells[5].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +"  "+
									document.getElementById ('normalxsl').rows[row].cells[6].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +"  "+
									document.getElementById ('normalxsl').rows[row].cells[7].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +";"+"\r\n";
					//results.push(
					//			document.getElementById ('normalxsl').rows[row].cells[2].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
					//			document.getElementById ('normalxsl').rows[row].cells[3].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
					//			document.getElementById ('normalxsl').rows[row].cells[4].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
					//		document.getElementById ('normalxsl').rows[row].cells[6].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,""));
				}
			}
		}       
         
        //results=results.replace("<br/>",/\n/g)
        //var  resultstring= results.replace(/<[^>]+>/g,"").replace(/\s*/g,"");
        var oInput = document.createElement('textarea');
        //oInput.value = results.join(",");
		oInput.value = results;
        document.body.appendChild(oInput);
        oInput.select(); // 选择对象
        document.execCommand("Copy"); // 执行浏览器复制命令
        oInput.className = 'oInput';
        oInput.style.display='none';
        alert('复制成功');
   };
   
   //复制普通生化XSlt模板(异常结果)
	function copyNormalUnusual () {
           
         var namebox = $("input[name^='boxs']");  //获取name值为boxs的所有input
         //var results = [];
		 var results = "";
		 //theResultYouWant--模板里配置的隐藏项，value值为想要复制的列，用逗号隔开
		var theResultsYouWant=document.getElementById ('theResultYouWant') ;
		if(theResultsYouWant && theResultsYouWant.value){
			var wantResultsColumn = theResultsYouWant.value;
			var resultsColumnList = wantResultsColumn.split(",");
			for(i = 0; i < namebox.length; i++) {      
				var row = namebox[i].getAttribute("rownum");
				var status = document.getElementById ('normalxsl').rows[row].cells[5].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"");
				if( status== "↑" || status== "↑↑" || status== "↓" || status== "↓↓" || status== "L" || status== "LL"|| status== "H" || status== "HH"){
					for(j = 0; j < resultsColumnList.length; j++){
						results += document.getElementById ('normalxsl').rows[row].cells[resultsColumnList[j]].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +"  ";
					}
					results +=";"+"\r\n";
				}
			}
		}else{
			for(i = 0; i < namebox.length; i++) {      
				var row = namebox[i].getAttribute("rownum");
				var status = document.getElementById ('normalxsl').rows[row].cells[5].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"");
				if( status== "↑" || status== "↑↑" || status== "↓" || status== "↓↓" || status== "L" || status== "LL"|| status== "H" || status== "HH"){
					results += row +" "+
						document.getElementById ('normalxsl').rows[row].cells[2].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
							document.getElementById ('normalxsl').rows[row].cells[3].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
							document.getElementById ('normalxsl').rows[row].cells[4].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
							document.getElementById ('normalxsl').rows[row].cells[5].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
							document.getElementById ('normalxsl').rows[row].cells[6].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
						   document.getElementById ('normalxsl').rows[row].cells[7].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +";"+"\r\n";
					//results.push(
					//		document.getElementById ('normalxsl').rows[row].cells[2].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
					//		document.getElementById ('normalxsl').rows[row].cells[3].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
					//		document.getElementById ('normalxsl').rows[row].cells[4].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,"") +" "+
					//		document.getElementById ('normalxsl').rows[row].cells[6].innerHTML.replace(/<[^>]+>/g,"").replace(/\s*/g,""));
				}
			}
		}
         
        //var  resultstring= results.replace(/<[^>]+>/g,"").replace(/\s*/g,"");
        var oInput = document.createElement('textarea');
        //oInput.value = results.join(",");
		oInput.value = results;
        document.body.appendChild(oInput);
        oInput.select(); // 选择对象
        document.execCommand("Copy"); // 执行浏览器复制命令
        oInput.className = 'oInput';
        oInput.style.display='none';
        alert('复制成功');
   };
function Call_HIS_Data() {
    var maysonBean = JSON.parse($("#Call_HIS_Data").html());
    var maysonAutherEntity={
		    autherKey: 'E81FCB8BCA78C521D1685531DC67009F', //必填，由惠每提供
			userGuid: maysonBean.userGuid, //必填，接入方自定义
			doctorGuid: maysonBean.doctorGuid, //必填，接入方自定义
			serialNumber: maysonBean.serialNumber, //必填，接入方自定义（必需与his系统病历号一至）
			department: maysonBean.inpatientDepartment, //科室（如“妇产科”，“儿科”）
			doctorName: maysonBean.doctorName,   	//医生名字
			hospitalGuid: '42500763801', //必填，如：医院名称拼音
			hospitalName: '松江中心医院' 	//自定义非必要字段 
		};
		HM.maysonLoader(maysonAutherEntity, function (mayson) {
	        mayson.setDrMaysonConfig('m',1); 
	        
			mayson.listenViewData = function (data) {
				if(data){
					for(var i=0;i<data.length;i++){
						var perData=data[i];
						if(perData.type==11){//type=11:惠每文献，返回url路径
							if(perData.items){
								for(var j=0;j<perData.items.length;j++){
									window.open(perData.items[j].text);
								}
							}
						}else{//文献外的其他类型，直接做回显处理
							if(perData.items){
								for(var j=0;j<perData.items.length;j++){
									window.alert(perData.items[j].text);
								}
							}
						}
					}
				}
			};

			mayson.setMaysonBean(maysonBean);
			mayson.ai();	
			
		});
};