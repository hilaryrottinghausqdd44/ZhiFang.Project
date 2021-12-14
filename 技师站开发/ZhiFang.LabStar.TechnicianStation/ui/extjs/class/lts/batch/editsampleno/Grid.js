/**
 * 批量样本号更改
 * @author liangyl
 * @version 2021-02-22
 */
Ext.define('Shell.class.lts.batch.editsampleno.Grid',{
    extend:'Shell.class.lts.batch.Grid',
    requires: ['Ext.ux.CheckColumn'],
    title:'批量样本号更改',
    width:810,
    height:500,
    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true',
    // 样本号批量修改
    updateUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormSampleNoModify',
		//显示成功信息
	showSuccessInfo:false,
	//消息框消失时间
	hideTimes:3000,
	
	//默认加载
	defaultLoad:false,
	//默认每页数量
	defaultPageSize:50,
	/**是否启用序号列*/
	hasRownumberer:true,
	//是否启用刷新按钮
	hasRefresh:true,
	defaultOrderBy:[{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}],
    /**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hasPagingtoolbar:false,
	//检验日期默认值
	defaultDate:null,
	//小组ID
	SectionID:'',
	//fieldset布局
	fieldsetLayout:{type:'table',columns:4},
	//fieldset内部组件初始属性
	fieldsetDefaults: {labelWidth:70,width:180,labelAlign:'right'},
	//fieldset 宽度			
	fieldsetWidth:760,
	  //按钮是否可点击
	BUTTON_CAN_CLICK: true,
	//原样本号
	OldSampleNoStr:"",
	defaultWhere :'listestform.MainStatusID>=0',
	//检验单范围所产生的样本号
	TestFormRangeSampleNo:"",
	//目标样本批量生成的样本号
	TSampleNoList:[],
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//计算截止样本号
		me.initSampleNoListeners();
	},
	initComponent:function(){
		var me = this;
		//初始化检验日期
		me.initDate();
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'检验单ID',dataIndex:'LisTestForm_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'原检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			sortable:false,menuDisabled:true,isDate:true,defaultRenderer:true
		},{
			text:'原样本号',dataIndex:'LisTestForm_GSampleNo',width:100,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				meta.style = 'color:red;';
			    if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
		        return value;
			}
		},{
			text:'条码号',dataIndex:'LisTestForm_BarCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'姓名',dataIndex:'LisTestForm_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'状态',dataIndex:'LisTestForm_MainStatusID',width:60,hidden:false,
			sortable:false,menuDisabled:true,
			renderer: me.onStatusRenderer
		},{
			xtype:'checkcolumn',text:'能否执行',dataIndex:'LisTestForm_IsExec',
			width:65,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'→',dataIndex:'Direction',width:40,align:'center',
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'目标检验日期',dataIndex:'TLisTestForm_GTestDate',width:85,
			sortable:false,menuDisabled:true,isDate:true,defaultRenderer:true
		},{
			text:'目标样本号',dataIndex:'TLisTestForm_GSampleNo',width:100,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				meta.style = 'color:red;';
			    if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
		        return value;
			}
		}];
		
		return columns;
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults,
			items = [{
		        xtype: 'displayfield',
		        fieldLabel: '',margin: '10 0 15 10',
		        name: '',
		        value: '<span style="font-weight:bold;color:blue;">提示:样本还没有检验，只更改样本号</span>'
		    },{
	            xtype:'fieldset',title: '检验单当前样本号',collapsible: true,itemId: 'CFieldSet',
	            defaultType: 'textfield',layout:layout,defaults:defaults,width:me.fieldsetWidth,
	            items :[{
	                 xtype: 'datefield',format: 'Y-m-d',fieldLabel:'检验日期',emptyText: '检验日期',itemId:'GTestDate',value:me.defaultDate
	            },{
	                fieldLabel:'开始样本号',itemId:'GSampleNo',emptyText: '开始样本号'
	            },{
		        	fieldLabel:'检验单数量',itemId:'GSampleNoForOrder',xtype:'numberfield',emptyText: '检验单数量'
		        },{
	                xtype:'displayfield',fieldLabel:'截止样本号',itemId:'EndGSampleNo'
	            }]
	        },{
	            xtype:'fieldset',title: '检验单目标样本号',collapsible: true,itemId: 'TCFieldSet',
	            defaultType: 'textfield',layout:layout,defaults:defaults,width:me.fieldsetWidth,
	            items :[{
	                 xtype: 'datefield',format: 'Y-m-d',fieldLabel:'检验日期',emptyText: '检验日期',itemId:'TGTestDate',value:me.defaultDate
	            },{
	                fieldLabel:'开始样本号',itemId:'TGSampleNo',emptyText: '开始样本号'
	            },{
	                xtype:'displayfield',fieldLabel:'截止样本号',itemId:'TEndGSampleNo'
	            },{
			    	xtype: 'button',text: '确认',tooltip: '确认',iconCls: 'button-accept',width:65, margin: '0 0 0 80',
					handler:function(but,e){
						
						var buttonsToolbar = me.getComponent('buttonsToolbar'),
						    CFieldSet = buttonsToolbar.getComponent('CFieldSet'),
						    GTestDate = CFieldSet.getComponent('GTestDate'),
						    TCFieldSet = buttonsToolbar.getComponent('TCFieldSet'),
						    TGTestDate = TCFieldSet.getComponent('TGTestDate');
		                 //时间校验
                        if(!GTestDate.isValid()) {
				        	me.hideMask();
				        	return;
				        }
                        if(!TGTestDate.isValid()) {
				        	me.hideMask();
				        	return;
				        }
						me.onGridSearch();
			        }
				}]
	        }
	    ];

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			//布局方式
			layout:'anchor',
			items: items
		});
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'toolbar',dock:'bottom',itemId:'bottombuttonsToolbar',
			items:[ {
				xtype: 'button',text: '执行',tooltip: '执行',iconCls: 'button-accept',
				handler: function() {
					me.onMerge();
				}
			},{text:'关闭',tooltip:'关闭',iconCls:'button-del',
	            handler:function(but,e){
	            	if (me.BUTTON_CAN_CLICK)me.close();
			    }
	        }]
		};
		return items;
	},
	/**初始化检验日期*/
	initDate: function() {
		var me = this;
		var Sysdate = JShell.System.Date.getDate();
		me.defaultDate =  JShell.Date.toString(Sysdate,true)
	},
	getSearchObj : function(){
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
		    CFieldSet = buttonsToolbar.getComponent('CFieldSet'),
		    GTestDate = CFieldSet.getComponent('GTestDate').getValue(),
		    GSampleNo = CFieldSet.getComponent('GSampleNo').getValue(),
		    GSampleNoForOrder = CFieldSet.getComponent('GSampleNoForOrder').getValue(),
		    TCFieldSet = buttonsToolbar.getComponent('TCFieldSet'),
		    TGTestDate = TCFieldSet.getComponent('TGTestDate').getValue(),
		    TGSampleNo = TCFieldSet.getComponent('TGSampleNo').getValue();
		return {
			GTestDate:JShell.Date.toString(GTestDate,true),
			GSampleNo:GSampleNo,
			GSampleNoForOrder:GSampleNoForOrder,
			TGTestDate:JShell.Date.toString(TGTestDate,true),
			TGSampleNo:TGSampleNo
		}
	},
	//合并前校验
	isValidMerge : function(){
		var me = this,
		    msg = '',str1='',str2='';
		var obj = me.getSearchObj();
        if(!obj.GTestDate)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验日期不不能为空!';
        //判断日期是否有效
//		if (!(obj.GTestDate instanceof Date))str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的日期!';
        if(!obj.GSampleNo)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开始样本号不能为空!';
        if(!obj.GSampleNoForOrder)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验单数量不能为空!';
        if(str1)msg+='</br><span style="font-weight:bold;color:blue;">检验单当前样本号的</span>'+str1;
		if(!obj.TGTestDate)str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验日期不不能为空!';
		 //判断日期是否有效
//		if (!(obj.TGTestDate instanceof Date))str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的日期!';
      
        if(!obj.TGSampleNo)str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开始样本号不能为空!';
        if(str2)msg+='</br><span style="font-weight:bold;color:blue;">检验单目标样本号的</span>'+str2;
        return msg;
	},
	//根据当前样本号和目标样本号合并检验单
	onMerge : function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.updateUrl;
	    
	    if(!me.BUTTON_CAN_CLICK)return;
		//确定前验证
	    var items = me.store.data.items;
	    if(items.length==0)	{
			JShell.Msg.warning('没有找到检验单');
			return;
		}
	    var obj = me.getSearchObj();
	    me.BUTTON_CAN_CLICK = false;
		var entity ={
			sectionID:me.SectionID,
			curTestDate:obj.GTestDate,
			curMinSampleNo:obj.GSampleNo,
			sampleCount:obj.GSampleNoForOrder,
			targetTestDate:obj.TGTestDate,
			targetMinSampleNo:obj.TGSampleNo
		};
		//保存到后台
		JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
			me.BUTTON_CAN_CLICK = true;
			if(data.success){
				me.fireEvent('save',me);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	  
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,params = [],
			obj = me.getSearchObj();
	
		//小组Id
		if(me.SectionID) {
			params.push("listestform.LBSection.Id=" + me.SectionID + "");
		}
		if(obj.GTestDate){
			params.push("listestform.GTestDate='" + obj.GTestDate + "'");
		}
		if(me.TestFormRangeSampleNo){
			params.push("listestform.GSampleNo in(" + me.TestFormRangeSampleNo + ")");
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this,
		    obj = me.getSearchObj(),
		    msg = "",SampleNoArr=[];
    	for( var i=0;i<data.list.length;i++){
			data.list[i].TLisTestForm_GTestDate = obj.TGTestDate;
			data.list[i].TLisTestForm_GSampleNo = me.TSampleNoList[i];
			data.list[i].Direction ='→';
			data.list[i].LisTestForm_IsExec = "true";
			if(data.list[i].LisTestForm_MainStatusID!=0){
				data.list[i].LisTestForm_IsExec = "false";
				SampleNoArr.push('【'+data.list[i].LisTestForm_GSampleNo+'】');
			}
		}
    	if(SampleNoArr.length>0){
    		msg+='原样本号为'+SampleNoArr.join(',')+'的检验单状态不是检验中,不能执行批量样本号更改!';
    		JShell.Msg.warning(msg);
    	}
    	//data.list = list;
    	
		return data;
	},

	initSampleNoListeners:function(){
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
		    CFieldSet = buttonsToolbar.getComponent('CFieldSet'),
		    GSampleNo = CFieldSet.getComponent('GSampleNo'),
		    EndGSampleNo = CFieldSet.getComponent('EndGSampleNo'),
		    GSampleNoForOrder = CFieldSet.getComponent('GSampleNoForOrder'),
		    TCFieldSet = buttonsToolbar.getComponent('TCFieldSet'),
		    TGSampleNo = TCFieldSet.getComponent('TGSampleNo'),
		    TEndGSampleNo= TCFieldSet.getComponent('TEndGSampleNo');
		
		//样本号改变
		GSampleNo.on({
			change : function(com,newValue,oldValue,eOpts ){
				EndGSampleNo.setValue('');
				if(GSampleNoForOrder.getValue() && newValue){
					//查询样本截止样本单号
				    me.endSampleNo(newValue,GSampleNoForOrder.getValue(),EndGSampleNo);
				}
			}
		});
		
		//数量改变
		GSampleNoForOrder.on({
			change : function(com,newValue,oldValue,eOpts ){
				EndGSampleNo.setValue('');
				TEndGSampleNo.setValue('');
				if(GSampleNo.getValue() && newValue){
					//查询样本截止样本单号
				    me.endSampleNo(GSampleNo.getValue(),newValue,EndGSampleNo);
				}
				if(TGSampleNo.getValue() && newValue){
					//查询目标样本截止样本单号
				    me.endSampleNo(TGSampleNo.getValue(),newValue,TEndGSampleNo);
				}
			}
		});
		
		//目标样本号改变
		TGSampleNo.on({
			change : function(com,newValue,oldValue,eOpts ){
				TEndGSampleNo.setValue('');
				if(GSampleNoForOrder.getValue() && newValue){
					//查询目标样本截止样本单号
				    me.endSampleNo(newValue,GSampleNoForOrder.getValue(),TEndGSampleNo);
				}
			}
		});
	},
	//确认查询
	onGridSearch:function(){
		var me = this;
		//确定前验证
	    var msg = me.isValidMerge(),
	    	obj = me.getSearchObj();
	    if(msg)	{
			JShell.Msg.warning(msg);
			return;
		}
	    //查询样本号批量生成的新样本号
	    me.batchCreateSampleNo(obj.GSampleNo,obj.GSampleNoForOrder,function(data){
	    	if(!data){
	    		JShell.Msg.warning('请输入正确的样本号格式');
	    		return;
	    	}
	    	var arr = data.split(',');
	    	me.TestFormRangeSampleNo ="";
            for(var i=0;i<arr.length;i++){
            	if(me.TestFormRangeSampleNo.length>1)me.TestFormRangeSampleNo+=",";
            	me.TestFormRangeSampleNo+="'"+arr[i]+"'";
            }
            me.TSampleNoList=[];
            //查询目标样本号批量生成的新样本号
            me.batchCreateSampleNo(obj.TGSampleNo,obj.GSampleNoForOrder,function(data2){
	    	     if(!data2){
		    		JShell.Msg.warning('请输入正确的样本号格式');
		    		return;
		    	}
	    	    me.TSampleNoList = data2.split(',');
	    	    me.onSearch();
	    	});
	    });
	}
});