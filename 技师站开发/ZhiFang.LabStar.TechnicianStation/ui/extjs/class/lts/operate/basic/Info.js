
/**
 * 检验确认人、审核人设置
 * @author liangyl
 * @version 2020-05-08
 */
Ext.define('Shell.class.lts.operate.basic.Info', {
	extend:'Shell.ux.form.Panel',
    formtype:'add',
	title:'检验确认人设置',
	width:665,
	height:400,
	bodyPadding:'20px 10px',
	//布局方式
	layout:'anchor',
	//每个组件的默认属性
	defaults:{
		anchor:'100%',
		labelWidth:60,
		labelAlign:'right'
	},
    //按钮是否可点击
    BUTTON_CAN_CLICK:true, 
    //查操作授权对应小组
    selectUrl2:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateASectionByHQL?isPlanish=true',
    //查操作授权
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateAuthorizeById?isPlanish=true',
	SectionID:null,
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		JShell.System.ClassDict.init('ZhiFang.Entity.LabStar','AuthorizeType',function(){
			if(!JShell.System.ClassDict.AuthorizeType){
    			JShell.Msg.error('未获取到授权类型,请重新刷新');
    			return;
    		}
	    });
		//自定义按钮功能栏
		me.buttonToolbarItems = [{text:'采用',tooltip:'采用',iconCls:'button-accept',
			handler:function(){
				//确定选择行
				me.onAcceptClick();
			}
		}];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [{
				fieldLabel: '授权详细内容',height: 155,labelAlign: 'top',name: 'Comment',xtype: 'textarea'
			},{
		        xtype: 'checkboxgroup',
		        fieldLabel: '授权小组',itemId:'SectionID',
		        columns: 2,vertical: true,
		        vertical: true,
		        items: []
		   },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_Id',name:'LisOperateAuthorize_Id',fieldLabel:'',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_OperateType',name:'LisOperateAuthorize_OperateType',fieldLabel:'授权操作类型',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_OperateTypeID',name:'LisOperateAuthorize_OperateTypeID',fieldLabel:'授权人',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_AuthorizeUserID',name:'LisOperateAuthorize_AuthorizeUserID',fieldLabel:'授权人',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_OperateUserID',name:'LisOperateAuthorize_OperateUserID',fieldLabel:'被授权人',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_AuthorizeUser',name:'LisOperateAuthorize_AuthorizeUser',fieldLabel:'授权人',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_OperateUser',name:'LisOperateAuthorize_OperateUser',fieldLabel:'被授权人',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_BeginTime',name:'LisOperateAuthorize_BeginTime',fieldLabel:'授权开始时间',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_EndTime',name:'LisOperateAuthorize_EndTime',fieldLabel:'授权开始时间',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_AuthorizeInfo',name:'LisOperateAuthorize_AuthorizeInfo',fieldLabel:'授权开始时间',hidden:true },
		   {xtype:'textfield',itemId:'LisOperateAuthorize_AuthorizeType',name:'LisOperateAuthorize_AuthorizeType',fieldLabel:'授权类型',hidden:true },
		   {xtype:'textfield',name:'LisOperateAuthorize_Day1',fieldLabel:'周一',hidden:true },
		   {xtype:'textfield',name:'LisOperateAuthorize_Day2',fieldLabel:'周二',hidden:true },
		   {xtype:'textfield',name:'LisOperateAuthorize_Day3',fieldLabel:'周三',hidden:true },
		   {xtype:'textfield',name:'LisOperateAuthorize_Day4',fieldLabel:'周四',hidden:true },
		   {xtype:'textfield',name:'LisOperateAuthorize_Day5',fieldLabel:'周五',hidden:true },
		   {xtype:'textfield',name:'LisOperateAuthorize_Day6',fieldLabel:'周六',hidden:true },
		   {xtype:'textfield',name:'LisOperateAuthorize_Day0',fieldLabel:'周日',hidden:true },

		];
		return items;
	},
   
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	//根据授权操作id查询存在关系小组，并赋值
	loadDataByID:function(id){
		var me =  this;
		me.getComponent('SectionID').removeAll();
		me.getOperateASection(id,function(list){
			var checkIds = [],itemlist = [];
			for(var i=0;i<list.length;i++){
				checkIds.push(list[i].LisOperateASection_LBSection_Id);
				itemlist.push({ boxLabel:list[i].LisOperateASection_LBSection_CName, name: 'LBSection', inputValue: list[i].LisOperateASection_LBSection_Id,checked: true});
			}
			me.getComponent('SectionID').add(itemlist);
		});
	},
	//根据授权操作Id和时间范围内找出已被授权的 操作授权对应小组
	getOperateASection:function(Id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl2;
		var sysdate = JShell.System.Date.getDate();
	    var EndTime = JShell.Date.toString(sysdate);
		url += '&fields=LisOperateASection_LBSection_CName,LisOperateASection_LBSection_Id';
		url+="&where=lisoperateasection.LisOperateAuthorize.Id="+Id;
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = data.value ? data.value.list : [];
				callback(list);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var info = JShell.System.ClassDict.getClassInfoById('AuthorizeType',data.LisOperateAuthorize_AuthorizeType);
		var info2 = JShell.System.ClassDict.getClassInfoByName('AuthorizeType', '周期');
        var strComment = '授权操作类型:'+data.LisOperateAuthorize_OperateType+'\n';
        var AuthorizeUser ="";
        strComment+= '授权人:'+data.LisOperateAuthorize_AuthorizeUser+'\n';
        var OperateUser ="";
        strComment+= '被授权人:'+data.LisOperateAuthorize_OperateUser+'\n';
        if(info2.Id == data.LisOperateAuthorize_AuthorizeType){//周期 授权时段不显示日期,周期需要显示周几
        	var dayArr=[];
        	if(data.LisOperateAuthorize_Day1)dayArr.push('周一');
        	if(data.LisOperateAuthorize_Day2)dayArr.push('周二');
        	if(data.LisOperateAuthorize_Day3)dayArr.push('周三');
        	if(data.LisOperateAuthorize_Day4)dayArr.push('周四');
        	if(data.LisOperateAuthorize_Day5)dayArr.push('周五');
        	if(data.LisOperateAuthorize_Day6)dayArr.push('周六');
        	if(data.LisOperateAuthorize_Day0)dayArr.push('周日');
        	var strDay = dayArr.join(',') || "";
            strComment+= '周期:'+strDay+'\n';
        	var arr1 = data.LisOperateAuthorize_BeginTime.split(' ');	
            var arr2 = data.LisOperateAuthorize_EndTime.split(' ');	
            var strDateTime = arr1[1]+'-'+arr2[1];
        	strComment+= '授权时间段:'+strDateTime+'\n';
        }else{
        	var strDateTime = JShell.Date.toString(data.LisOperateAuthorize_BeginTime)+'-'+JShell.Date.toString(data.LisOperateAuthorize_EndTime);
        	strComment+= '授权时间段:'+strDateTime+'\n';
        }
        strComment+= '授权说明:'+data.LisOperateAuthorize_AuthorizeInfo+' \n';
        strComment+= '授权类型:'+info.Name;
        data.Comment=strComment;
		return data;
	},
	onAcceptClick : function(){
		var me = this,
			values = me.getForm().getValues();
		//如果是周期，1970-01-01 替换为当前日期,如果开始时间小于结束时间，开始时间往前推一天
		var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType', '周期');
        var BeginTime = values.LisOperateAuthorize_BeginTime;
        var EndTime = values.LisOperateAuthorize_EndTime;
        if(info.Id ==values.LisOperateAuthorize_AuthorizeType ){//周期
        	var sysdate = JShell.System.Date.getDate();
	        var end = EndTime.split(" ");
	        var start = BeginTime.split(" ");
        	//开始时间大于结束时间时，需要把上一天的数据也给显示出来
            if(JShell.Date.getDate(BeginTime)>JShell.Date.getDate(EndTime)){
            	//上一天日期
            	var preday = JShell.Date.getNextDate(JShell.Date.toString(sysdate,true),-1);
            	BeginTime = JShell.Date.toString(preday,true)+ ' '+start[1];
            	EndTime = JShell.Date.toString(sysdate,true)+ ' '+end[1];
            }else{
                BeginTime = JShell.Date.toString(sysdate,true)+ ' '+start[1];
            	EndTime = JShell.Date.toString(sysdate,true)+ ' '+end[1];
            }
        }
        //是否包含当前小组
        var isCurrSection = true;
        //授权小组是否包含当前小组
        var ids = values.LBSection;       
        if(!ids)isCurrSection=false;
        var type = Ext.typeOf(ids);
		if (type == 'array'){
			if(ids.indexOf(me.SectionID)==-1)isCurrSection=false;
		}
	    var obj={
			AuthorizeUserID:values.LisOperateAuthorize_AuthorizeUserID,//检验确认人ID
			AuthorizeUserName:values.LisOperateAuthorize_AuthorizeUser,//检验确认人
			BeginTime:BeginTime,//预授权开始时间
			EndTime:EndTime,//预授权结束时间
			AuthorizeType:values.LisOperateAuthorize_AuthorizeType, //授权类型
			Day1:values.LisOperateAuthorize_Day1,//周一
			Day2:values.LisOperateAuthorize_Day2,
			Day3:values.LisOperateAuthorize_Day3,
			Day4:values.LisOperateAuthorize_Day4,//周一
			Day5:values.LisOperateAuthorize_Day5,
			Day6:values.LisOperateAuthorize_Day6,
			Day0:values.LisOperateAuthorize_Day0,
            CurrSection:isCurrSection
		};
		me.fireEvent('accept', obj);
	}
});