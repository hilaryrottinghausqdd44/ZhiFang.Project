Ext.define('Shell.class.CheckReportRequest.basic.ProjectContrast',{
	extend:'Ext.window.Window',
    id: 'myPanel',
 	name: 'myPanel',
 	autoScroll:true,
 	PatNO:'',
 	obresponse:'',
 	afterRender:function(){
	    var me = this;
		me.callParent(arguments);
		me.addhtmlcontent = me.addhtmlcontent(me.obresponse);
	},
	initComponent:function(){
		var me = this;
		me.dockedItems = [{
    	xtype:'toolbar',
    	dock:'top',
    	itemId:'ss',
    	items:[
    		{xtype:'uxdatearea',itemId:'date'},
			{
				xtype:'uxbutton',
				itemId:'search',
				iconCls:'button-search',
				tooltip:'<b>查询</b>',
				handler:function(){
					var DateTime= me.getComponent('ss').getComponent('date').getValue();
					if(DateTime){
						var startTime = Ext.Date.format(DateTime.start,'Y-m-d H:i:s');
						var endTime = Ext.Date.format(DateTime.end,'Y-m-d H:i:s');
						var where = "";
						if(startTime && endTime){
							where += "ReceiveDate>='"+startTime+"' and ReceiveDate <'"+endTime+"'";
						}else if(startTime && !endTime){
							where += "ReceiveDate>='"+startTime+"'";
						}else if(!startTime && endTime){
							where += "ReceiveDate<'"+endTime+"'";
						}
						var data = {PatNO:me.PatNO,Where:where};
						Ext.Ajax.defaultPostHeader = 'application/json';
						Ext.Ajax.request({
							method: 'POST',
							async: false,
						    url: Shell.util.Path.rootPath +'/ServiceWCF/ReportFormService.svc/ResultDataTimeMhistory',
						    params:Ext.JSON.encode(data),
							success: function(response2){
							    var response2 = Ext.decode(response2.responseText);
								var obresponse2 = JSON.parse(response2.ResultDataValue);
								if(response2.success==true){
									me.temp.overwrite(Ext.get('htmlcontent'));
								    me.buildTable(obresponse2);
								}else{
									//alert("日期选择有误，请重新选择!");
									Shell.util.Msg.showInfo("日期选择有误，请重新选择！");
								}
							},
							error : function(response){
								//alert("执行错误");
								Shell.util.Msg.showInfo("执行错误！");
							}
						});
					}else{
						//alert('请选择时间');
						Shell.util.Msg.showInfo("请选择时间！");
					}
				}
			}
    	]
    }];
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
		}catch(e){
			alert(e);
		}
		
		
    },
    buildTable:function(arr){
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
			var ReceiveDate = arr[i*2][0].ReceiveDate;
			var CheckDate = arr[i*2][0].CheckDate.split(" ")[0];
			var CheckTime = arr[i*2][0].CheckTime.split(" ")[1];
			var h = "h"+i;
			sectionArr.push(h);
			map.SectionName = h;
			//map.set(SectionName,h);
			$("#thead tr").append('<th style="height:25px;font-size:13px;font-weight:bolder;" class='+h+'>'+SectionName+'('+CheckDate+' '+CheckTime+')'+'</th>');
	
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
					$("#tbody").append('<tr class='+me.cleanSign(ItemNo)+'><th style="height:25px;font-size:13px;font-weight:bolder;">'+ItemCname+'('+ItemNo+')'+'</th></tr>');
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
				$("."+h+ItemNo).text(ItemValue+" "+ItemUnit);
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