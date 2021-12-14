/**
 * 折线图
 * @author liangyl	
 * @version 2019-11-28
 */
Ext.define('Shell.class.lts.itemmerge.LineChart',{
    extend: 'Shell.ux.chart.EChartsPanel2',

	layout: 'fit',
	title: '折线图',
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemMergeImageData',
	uploadUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisTestItemMergeGraph',
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	//当前图片数据
	currData:[],
	//列表项目数组,从外边传入
	itemList:[],
	//图形高度
	GraphHeight:400,
	//图形宽度
	GraphWidth:300,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
    },
	initComponent: function() {
		var me = this;
		me.addEvents('cbChangeClick','save');
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
   		me.callParent(arguments);
		
	},
	//创建表单
	createItems:function(){
		var me = this;
		var items =[
			{xtype: 'filefield',fieldLabel: '附件',name: 'file'},
			{xtype: 'textfield',fieldLabel: '附件',name: 'graphThumb',itemId: 'graphThumb'},
			{xtype: 'textfield',fieldLabel: '附件',name: 'graphBase64',itemId: 'graphBase64'},
			{xtype: 'textfield',fieldLabel: 'testFormID',name: 'testFormID',itemId:'testFormID'},
			{xtype: 'textfield',fieldLabel: 'graphInfo',name: 'graphInfo',itemId:'graphInfo'},
			{xtype: 'textfield',fieldLabel: 'graphName',value:'测试',name: 'graphName',itemId:'graphName'}, 
			{xtype: 'textfield',fieldLabel: 'graphType',name: 'graphType',value:'.png'}, 
	        {xtype: 'textfield',fieldLabel: 'graphHeight',name: 'graphHeight',value:me.GraphHeight},
			{xtype: 'textfield',fieldLabel: 'graphWidth',name: 'graphWidth',value:me.GraphWidth}
		];
		me.FormPanel= Ext.create('Shell.ux.form.Panel', {
		    title: '需要保存的数据',bodyPadding: 10,
			layout: 'anchor',itemId:'FormPanel',hidden: true,
			/**每个组件的默认属性*/
			defaults: {
				anchor: '100%',
				labelWidth: 80,
				labelAlign: 'right',
				hidden: true
			},
			items: items
		});
		return [me.FormPanel];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		var toolitems=[];
	    toolitems.push( {
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '参考范围',
            name: 'cbRefRange',itemId:'cbRefRange',
            checked:true,
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            		me.fireEvent('cbChangeClick', me);
            	}
            }
        }, {
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '异常范围',
            name: 'cbAbnormalRange',itemId:'cbAbnormalRange',
            checked:true,
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            		me.fireEvent('cbChangeClick', me);
            	}
            }
        }, {
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '结果值提示',
            name: 'cbTip',itemId:'cbTip',
            checked:true,
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            		me.fireEvent('cbChangeClick', me);
            	}
            }
        }, {
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '最小值=0',
            name: 'cbMinVal',itemId:'cbMinVal',
            checked:true,hidden:true,
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            		me.fireEvent('cbChangeClick', me);
            	}
            }
        });
   
		items.push(Ext.create('Shell.ux.toolbar.Button',{
			dock:'bottom',
			itemId:'bottomToolbar',
			items:toolitems
		}));
		return items;
	},
	/**查询数据*/
	onSearch:function(params,items){
		var me = this;
		me.itemList = items;
		me.showMask(me.loadingText);//显示遮罩层
		me.clearData();
		var url =  JShell.System.Path.ROOT + me.selectUrl;
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.changeData(data.value,items);
			}else{
				me.showErrror("提示信息:"+data.msg);
			}
		});
	},
	//数据返回解析
    resultData:function(list,items){
    	var me = this;
    	var xAxisList =[];
        var PointValueList = [],PointRangeLList=[],PointRangeHList=[],PointYCRangeLList=[],listPointYCRangeHList=[];
        
        var cbRefRange = me.getComponent('bottomToolbar').getComponent('cbRefRange').getValue();
        var cbAbnormalRange = me.getComponent('bottomToolbar').getComponent('cbAbnormalRange').getValue();
        var cbMinVal = me.getComponent('bottomToolbar').getComponent('cbMinVal').getValue();

        for(var i = 0 ;i<list.length;i++){
        	var LineName = list[i].LineName;
             switch (LineName){
             	case "PointValue"://项目参考值
             	    var LinePoint = list[i].LinePoint;
             	    for(var j=0;j<LinePoint.length;j++){
             	    	for(var n=0;n<items.length;n++){
             	    		if(LinePoint[j].X==items[n].data.LBMergeItemVO_ChangeItemID){
             	    			xAxisList.push(items[n].data.LBMergeItemVO_ChangeItemName);
             	    			break;
             	    		}
             	    	}
             	    	PointValueList.push(LinePoint[j].Y);
             	    }
             		break;
             	case "PointRangeL"://项目参考值低值
             	    if(cbRefRange){
             	    	var PointRangeL = list[i].LinePoint;
	             	    for(var j=0;j<PointRangeL.length;j++){
	             	    	PointRangeLList.push(PointRangeL[j].Y);
	             	    }
             	    }
             	    break;
             	case "PointRangeH"://项目参考值高值
             	    if(cbRefRange){
	             	    var PointRangeH = list[i].LinePoint;
	             	    for(var j=0;j<PointRangeH.length;j++){
	             	    	PointRangeHList.push(PointRangeH[j].Y);
	             	    }
             	    }
             		break;
             	case "PointYCRangeL"://项目参考值异常低值
             	    if(cbAbnormalRange){
             	    	var PointYCRangeL = list[i].LinePoint;
	             	    for(var j=0;j<PointYCRangeL.length;j++){
	             	    	PointYCRangeLList.push(PointYCRangeL[j].Y);
	             	    }
             	    }
             		break;
             	case "listPointYCRangeH"://项目参考值异常高值
             	    if(cbAbnormalRange){
	             	    var listPointYCRangeH = list[i].LinePoint;
	             	    for(var j=0;j<listPointYCRangeH.length;j++){
	             	    	listPointYCRangeHList.push(listPointYCRangeH[j].Y);
	             	    }
             	    }
             		break;
             	default:
             		break;
             }
        }
        return {
        	xAxisList:xAxisList,
        	PointValueList:PointValueList,
        	PointRangeLList:PointRangeLList,
        	PointRangeHList:PointRangeHList,
        	PointYCRangeLList:PointYCRangeLList,
        	listPointYCRangeHList:listPointYCRangeHList
        }
    },
	/**
	 * 更改图表
	 */
	changeData: function(list,items) {
		var me = this;
        var xAxisList =[];
        me.currData=list;
        var PointValueList = [],PointRangeLList=[],PointRangeHList=[],PointYCRangeLList=[],listPointYCRangeHList=[];
        var items = me.resultData(list,items);
        xAxisList = items.xAxisList;
        PointValueList = items.PointValueList;
        PointRangeLList = items.PointRangeLList;
        PointRangeHList = items.PointRangeHList;
        PointYCRangeLList = items.PointYCRangeLList;
        listPointYCRangeHList = items.listPointYCRangeHList;
	    var cbTip = me.getComponent('bottomToolbar').getComponent('cbTip').getValue();
		var config = {
		    tooltip: {trigger: 'axis',show:false},
		    animation : false,
		    color:['#0000FF','#32CD32','#32CD32','#FF0000','#FF0000'],//关键加上这句话，legend的颜色和折线的自定义颜色就一致了
		    grid: {
		        left: '3%',
		        right: '4%',
		        bottom: '3%',
		        containLabel: true
		    },
		    xAxis: {type: 'category',boundaryGap: false,data: xAxisList},
		    yAxis: {type: 'value'},
		    series: [
	        { name:'项目参考值',type:'line',data:PointValueList,
	           showSymbol: true, symbol: 'circle',symbolSize: 10,   //设定实心点的大小
	           itemStyle : { normal: {label : {show: cbTip ? true : false}}}
	        },{
	            name:'项目参考范围低值', type:'line',data:PointRangeLList,
	            smooth:false,//关键点，为true是不支持虚线的，实线就用true
	            itemStyle:{
	                normal:{
	                    lineStyle:{
	                        width:2,type:'dotted'  //'dotted'虚线 'solid'实线
	                    }
	                }
	            }
	        },{
	            name:'项目参考范围高值',type:'line',
	            data:PointRangeHList,smooth:false,
	            itemStyle:{
	                normal:{
	                    lineStyle:{
	                        width:2,
	                        type:'dotted'  //'dotted'虚线 'solid'实线
	                    }
	                }
	            }
	        }, {
	            name:'项目参考范围异常低值', type:'line',data:PointYCRangeLList,smooth:false,
	            itemStyle:{
	                normal:{
	                    lineStyle:{
	                        width:2,
	                        type:'dotted'  //'dotted'虚线 'solid'实线
	                    }
	                }
	            }
	        },{
	            name:'项目参考范围异常高值',type:'line', data:listPointYCRangeHList,smooth:false,
	            itemStyle:{
	                normal:{
	                    lineStyle:{
	                        width:2,
	                        type:'dotted'  //'dotted'虚线 'solid'实线
	                    }
	                }
	            }
	        }]
		};
	   
		me.changeChart(config, true);
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
     /**保存图形*/
	onUploadImg : function(GroupItemObj){
		var me = this;
		var testFormID ="";
        for(var i=0;i<me.itemList.length;i++){
        	if(me.itemList[i].data.LBMergeItemVO_IsMerge==true){
        		testFormID = me.itemList[i].data.LBMergeItemVO_LisTestItem_LisTestForm_Id;
        		break;
        	}
        }
        var graphName ="";
        //图片名称 当有简称取简称 所以不从 me.itemList取值
        //名称
        if(GroupItemObj && GroupItemObj.LBItem_Id){
        	if(GroupItemObj.LBItem_SName){
        		graphName = GroupItemObj.LBItem_SName;
        	}else{
        		graphName = GroupItemObj.LBItem_CName;
        	}
        }
		JShell.Action.delay(function(){
			var imgbase64 = me.chart._api.getDataURL();
	        me.FormPanel.getForm().setValues({
				graphThumb : imgbase64,
				graphBase64 : imgbase64,
				testFormID : testFormID,
				graphInfo : Ext.encode(me.currData),
				graphName : graphName
			});
		    var url = JShell.System.Path.getRootUrl(me.uploadUrl);
	        me.FormPanel.getForm().submit({
				url: url,
				success: function(form, action) {
					me.hideMask(); //隐藏遮罩层
					var data = action.result;
					if(data.success) {
						me.fireEvent('save', me);
					} else {
						JShell.Msg.error(data.ErrorInfo);
					}
				},
				failure: function(form, action) {
					me.hideMask(); //隐藏遮罩层
					var data = action.result;
					JShell.Msg.error(data.ErrorInfo);
				}
			});
		},null,500);
	}
});