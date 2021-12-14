/**
 * 帮助显示
 * @author Jcall
 * @version 2018-03-01
 */
Ext.define('Shell.class.sysbase.main.Help',{
    extend:'Ext.panel.Panel',
    requires: ['Shell.ux.toolbar.Button'],
    title:'帮助文档',
    
    bodyPadding:5,
    height:800,
    width:1200,
    //获取文件服务
    selectFileUrl:'/ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileByHQL',
    //帮助系统所属的模块Id,外部调用传入
	ModuleId: null,
	//帮助文档所属模块的子序号,外部调用传入
	SubWinNo: null,
	//对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)
	IDS:'5529429566368103413',
	//默认挂载节点ID
	BDictTreeId:'5529429566368103413',
	//默认挂载节点名称
	BDictTreeCName:'联机帮助',
	//编辑文档类型(如新闻/通知/文档/修订文档)
	FTYPE: '5',
	//文件的操作记录类型
	fFileOperationType: 5,
	//功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布
	hiddenRadiogroupChoose: [false, true, true, true, true],
	//功能按钮默认选中
	checkedRadiogroupChoose: [false, false, false, true],
	//是否显示下一处理人
	hasNextExecutor: false,
	//文档信息
	_FileInfo:{
		FFile_Id:null,
		FFile_No:null,
		FFile_BDictTree_Id:null,
		FFile_BDictTree_CName:null,
		FFile_OriginalFileID:null,
		FFile_Status:null
	},
    
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	
    	var buttonsToolbar = me.getComponent('buttonsToolbar');
    	if(buttonsToolbar){
    		var add = buttonsToolbar.getComponent('add'),
    			edit = buttonsToolbar.getComponent('edit');
    			
    		add.hide();
    		edit.hide();
    	}
    	
    	me.onRefreshClick();
    },
    
    initComponent:function(){
    	var me = this;
    	//账号限制
    	var accountName = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME);
    	if(accountName == 'zf'){
    		me.dockedItems = me.createDockedItems();
    	}
    	//修改内容
    	me.onChangeContent();
    	
    	me.callParent(arguments);
    },
    //创建按钮栏
    createDockedItems:function(){
    	var me = this;
    	
    	var FileNo = 'H-' + me.ModuleId;
    	if(me.SubWinNo){
    		FileNo += '-' + me.SubWinNo;
    	}
    	
    	var dockedItems = [{
    		xtype:'uxButtontoolbar',
    		dock: 'top',
			itemId: 'buttonsToolbar',
			items: ['refresh','-','add','edit','-',{
				xtype:'label',
				itemId:'FileNo',
				text:'文档编码：' + FileNo
			}]
    	}];
    	return dockedItems;
    },
    //刷新
    onRefreshClick:function(){
    	var me = this;
    	//根据木块信息获取文件信息
    	me.getFileInfoByModule(function(){
    		me.onChangeContent();//修改内容
    	});
    },
    //新增
    onAddClick:function(){
		this.openFFileForm();
    },
    //修改
    onEditClick:function(){
    	this.openFFileForm();
    },
    //打开新增或编辑文档表单
	openFFileForm: function() {
		var me = this;
			OriginalFileID = "",
			BDictTreeId = me.BDictTreeId,
			BDictTreeCName = me.BDictTreeCName;
			
		//文档修改
		if(me._FileInfo.FFile_Id) {
			BDictTreeId = me._FileInfo.FFile_BDictTree_Id;
			BDictTreeCName = me._FileInfo.FFile_BDictTree_CName;
			OriginalFileID = me._FileInfo.FFile_OriginalFileID || me._FileInfo.FFile_Id;
		}
		
		var FileNo = 'H-' + me.ModuleId;
    	if(me.SubWinNo){
    		FileNo += '-' + me.SubWinNo;
    	}
    	var maxHeight = document.body.clientHeight;
		var maxWidth = document.body.clientWidth;
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: me.FTYPE.toString() + "1",
			height: maxHeight*0.9,
			width: maxWidth*0.9,
			hasReset: true,
			title: "帮助文档-新增",
			formtype: 'add',
			fFileStatus: 1,
			
			BDictTreeId: BDictTreeId,
			BDictTreeCName: BDictTreeCName,
			fFileOperationType: me.fFileOperationType,
			
			HiddenAgreeButton: false,//基本应用的文档确认(通过/同意)操作按钮是否显示
			AgreeButtonText: "发布",//基本应用的文档确认(通过/同意)操作按钮显示名称
			AgreeOperationType: 5,//基本应用的文档确认(直接发布)操作按钮的功能类型
			
			HiddenDisagreeButton: true,//基本应用的文档确认(不通过/不同意)操作按钮是否显示
			DisagreeButtonText: "撤消禁用",//基本应用的文档确认(不通过/不同意)操作按钮显示名称
			DisagreeOperationType: 1,//基本应用的文档确认(不通过/不同意)操作按钮的功能类型
			
			FTYPE: me.FTYPE,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			//LEVEL: me.LEVEL,
			
			//AppOperationType: me.AppOperationType,//--
			hiddenRadiogroupChoose: me.hiddenRadiogroupChoose,
			
			/**功能按钮默认选中*/
			checkedRadiogroupChoose: me.checkedRadiogroupChoose,
			hasNextExecutor: me.hasNextExecutor,
			isAddFFileReadingLog: 0,
			isAddFFileOperation: 1,
			basicFormApp: 'Shell.class.qms.file.help.release.Form',
			listeners: {
				save: function(win, e) {
					if(e.success) {
						me.onRefreshClick();
						me.isEdit();
						win.close();
					}
				},
				afterrender:function(win){
					this.basicForm.isAdd = function(){
						var FFile_No = this.getComponent('FFile_No');
						FFile_No.locked = true;
						FFile_No.setReadOnly(true);
						FFile_No.setValue(FileNo);
					}
				}
			}
		};
		if(me._FileInfo.FFile_Id) {
			config.formtype = 'edit';
			config.PK = me._FileInfo.FFile_Id;
			config.FFileId = me._FileInfo.FFile_Id;
			config.title = "帮助文档-编辑";
			config.fFileStatus = me._FileInfo.FFile_Status;
			config.OriginalFileID = OriginalFileID;
		}
		JShell.Win.open('Shell.class.qms.file.help.release.AddTabPanel', config).show();
	},
	//根据木块信息获取文件信息
	getFileInfoByModule:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectFileUrl + "?isPlanish=true";
			
    	var FileNo = 'H-' + me.ModuleId;
    	if(me.SubWinNo){
    		FileNo += '-' + me.SubWinNo;
    	}
    	var where = "ffile.No='" + FileNo + "'";
    	url += '&where=' + where;
    	
    	var fields = ['FFile_Id','FFile_No','FFile_Status','FFile_BDictTree_Id','FFile_BDictTree_CName','FFile_OriginalFileID'];
    	url += "&fields=" + fields.join(",");
    	
    	JShell.Server.get(url,function(data){
    		if(data.success){
    			var list = (data.value || {}).list || [],
    				len = list.length;
    			if(len == 0){
    				//JShell.Msg.error("帮助文档不存在！");
    				me.isAdd();
    			}else if(len == 1){
    				me._FileInfo = list[0];
    				me.isEdit();
    			}else{
    				JShell.Msg.error("数据存在错误，帮助文档存在多个，请联系管理员！");
    			}
    		}else{
    			JShell.Msg.error(data.msg);
    		}
    		callback();
    	});
	},
	//新增状态
	isAdd:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
    		add = buttonsToolbar.getComponent('add'),
    		edit = buttonsToolbar.getComponent('edit');
    		
    	add.show();
    	edit.hide();
	},
	//修改状态
	isEdit:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
    		add = buttonsToolbar.getComponent('add'),
    		edit = buttonsToolbar.getComponent('edit');
    		
    	add.hide();
    	edit.show();
	},
    //修改内容
    onChangeContent:function(){
    	var me = this;
    	var FileNo = 'H-' + me.ModuleId;
    	if(me.SubWinNo){
    		FileNo += '-' + me.SubWinNo;
    	}
    	var url = JcallShell.System.Path.ROOT + '/help/html/' + FileNo + '/index.html?v=' + new Date().getTime();
    	
    	Ext.Ajax.request({
    		url: url,
			async: true,
			method: 'get',
			success: function(response, opts) {
				var html = 
				"<iframe src ='" + url + "' " +
					"height='100%' width='100%' frameborder='0' style='overflow:hidden;" +
					"overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:" +
					"absolute;top:0px;left:0px;right:0px;bottom:0px'>" +
				"</iframe>";
				me.update(html);
			},
			failure: function(response, options) {
				var html = 
				'<div style="margin:20px;text-align:center;">' +
        			'<h1>文档编号：' + FileNo + '</h1>' +
        			'<b>帮助文档没有维护，请联系管理员！</b>' +
        		'</div>';
				me.update(html);
			}
    	});
    }
});