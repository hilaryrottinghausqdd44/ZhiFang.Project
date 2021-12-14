Ext.define('Shell.class.labStar.projectContrast.contrast.ProjectContrast',{
	extend:'Ext.panel.Panel',
    id: 'ProjectContrast',
 	name: 'ProjectContrast',
 	autoScroll:true,
 	PatNO:'',
 	obresponse:'',
 	border:0,
 	afterRender:function(){
	    var me = this;
		me.callParent(arguments);
		me.addhtmlcontent = me.addhtmlcontent(me.obresponse);
		if($("#tbody").find("tr:first").length > 0){
			printResult($("#tbody").find("tr:first")[0].className.replace("r",""));
		}
	},
	initComponent:function(){
		var me = this;
		
	    me.items= [{
	        xtype: 'fieldset',
	        id: 'htmlcontent',
	        name: 'htontent'
	    }];
		me.callParent(arguments);
	},
    addhtmlcontent:function(obresponse){
    	var me = this;
    	me.temp = new Ext.XTemplate(
		    '<table  border="1" width="100%" height="100%" cellspacing="0" cellpadding="0">',
		    	'<thead id="thead" >',
		    		'<tr style="height:30px;">',
		    			'<th style="width:200px;font-size:13px;font-weight:bolder;" class="first">',
		    					'<div class="jyxm" style="position:relative;padding-left:140px ;">检验小组</div>',
		    					'<div class="jyxz" style="position:relative;padding-left:10px">检验项目</div>',			    				
		    			'</th>',
		    		'</tr>',
		    		'</thead>',
		    		'<tbody id="tbody">',
				    '</tbody>',
		    '</table>');
		me.temp.compile();
		me.temp.overwrite(Ext.get('htmlcontent'));
		try{
			me.buildTable(obresponse);
		} catch (e) {
			Shell.util.Msg.showInfo(e);
		}
		
		
    },
	buildTable: function (arr) {
		if (!arr) {
			Shell.util.Msg.showInfo("未查询到数据！");
			return;
		}
		var me = this;
		var length = arr.length;
		var ItemNoArr = [];
		var sectionArr = [];
		//var map = new Map();
		var map = {};
		//加载列头
		for(var i = 0 ; i < length/2 ; i++){
			//获取列头名称
			var SectionName = arr[i*2][0].SectionName;
			//var ReceiveDate = arr[i*2][0].ReceiveDate;
			//var CheckDate = arr[i*2][0].CheckDate.split(" ")[0];
			//var CheckTime = arr[i*2][0].CheckTime.split(" ")[1];
			var h = "h"+i;
			sectionArr.push(h);
			map.SectionName = h;
			//map.set(SectionName,h);
			$("#thead tr").append('<th style="height:25px;font-size:13px;font-weight:bolder;" class='+h+'>'+SectionName+'</th>');
	
		}
	
		//加载行头
		for(var i = 0 ; i < length/2 ; i++){
			//获取行集合
			var Items = arr[i*2+1];
			for(var j = 0 ; j <Items.length ; j++){
				//获取行头名称
				var ItemCname = Items[j].ItemCname;
				var ItemNo = Items[j].ItemNo;
				if($.inArray(ItemNo,ItemNoArr)<0){
					ItemNoArr.push(ItemNo);
					var aa = "'"+ItemNo+"'";
					//$("#tbody").append('<tr class='+me.cleanSign(ItemNo)+'><th style="height:25px;font-size:13px;font-weight:bolder;">'+ItemCname+'('+ItemNo+')'+'</th></tr>');
					$("#tbody").append('<tr onclick="printResult('+aa+');" class="r'+me.cleanSign(ItemNo)+'"><th style="height:25px;font-size:13px;font-weight:bolder;">'+'&nbsp;&nbsp;&nbsp;'+ItemCname+'</th></tr>');
				}
			}
		}
	
		//补全表格
		//获取列头个数
		var sectionArrSize = sectionArr.length;
		//获取行头个数
		var ItemNoSize = ItemNoArr.length;
		for(var i = 0 ; i <sectionArrSize ; i++){
			var SectionClass =sectionArr[i];
			for(var j = 0 ; j <ItemNoSize ; j++){
				var ItemNoClass = ItemNoArr[j];
				$("#tbody tr").eq(j).append('<td class="'+SectionClass+ItemNoClass+'" style=""></td>');
			}
		
		}
	
		//生成表格完毕，绑定数据;
		me.bindTable(sectionArr,arr,map);
	},
	//绑定数据
	bindTable:function(sectionArr,arr,map){
		var me = this;
		var length = arr.length;
		for(var i = 0 ; i < length/2 ; i++){
			var SectionName = arr[i*2][0].SectionName;
			var h = map.SectionName;
			var Items = arr[i*2+1];
			for(var j = 0 ; j <Items.length ; j++){
				var ItemNo = Items[j].ItemNo;
				var ItemValue = Items[j].ItemValue;
				var ItemUnit = Items[j].ItemUnit;
				var ResultStatus = Items[j].ResultStatus;
				//$("."+h+ItemNo).text(ItemValue+" "+ItemUnit);
				$("."+h+ItemNo).text(" "+ItemValue);
				if(ResultStatus == '↑' || ResultStatus =='H'){
					$("."+h+ItemNo).css('color', 'red');
				}
				if(ResultStatus == '↓' || ResultStatus =='L'){
					$("."+h+ItemNo).css('color', 'blue');
				}
			}	
		}
	},
	//除去数据特殊字符供给唯一标示使用
	cleanSign:function(str){
		var signArr = ['%',"#","*"];
		var me = this;
		for(var i = 0 ; i < signArr.length ; i++){
			str = str.replace(signArr[i],'');
		}
		return str;
	}
		
})