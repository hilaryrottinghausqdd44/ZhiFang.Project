/**
 * 项目文档列表
 * @author liangyl	
 * @version 2017-04-06
 */
Ext.define('Shell.class.wfm.business.pproject.document.EditGrid', {
    extend: 'Shell.ux.grid.Panel',
    title: '项目文档列表',
    width: 700,
    height: 440,
    /**获取数据服务路径*/
    selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectDocumentByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePProjectDocumentByField',
    /**删除数据服务路径*/
    delUrl: '/SingleTableService.svc/ST_UDTO_DelPProjectDocument',
    
	addUrl:'/SingleTableService.svc/ST_UDTO_AddPProjectDocument',
    /**默认加载*/
    defaultLoad: true,
    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: false,
    hasSave:true,
    /**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否有删除*/
	hasDel:false,
	hasAdd:false,
	/**项目ID*/
	ProjectID:null,
	/**任务ID*/
	ProjectTaskID:null,
	/**是否启用序号列*/
	hasRownumberer: true,
		/**默认每页数量*/
	defaultPageSize: 200,
	/**带分页栏*/
	hasPagingtoolbar: false,

	/**是否显示文档详情页签*/
	hasFFileOperation: false,
    defaultOrderBy: [{ property: 'PProjectDocument_DispOrder', direction: 'ASC' }],
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.on({
			nodata:function(){
				me.getView().update('');
			}
		});
    },
    initComponent: function () {
        var me = this;
        me.regStr = new RegExp('"', "g");
        
        showProjectDocument = function(ele, e) {
        	var Id = ele.getAttribute("data");
        	me.showFFileById(Id);
		};
        //数据列
        me.columns = me.createGridColumns();
         //创建功能按钮栏Items
		if(me.hasButtontoolbar) me.buttonToolbarItems = me.createButtonToolbarItems();
   
        me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'FFileGridEditing'
		});
        me.callParent(arguments);
    },
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		//查询框信息
        me.searchInfo = {
            width: 145, emptyText: '项目名称/用户名称', isLike: true,itemId:'search',
            fields: ['PProjectDocument.CName','pproject.PClientName']
        };
		buttonToolbarItems.unshift('refresh','-');
		buttonToolbarItems.push({text:'选择',tooltip:'选择',iconCls:'button-edit',
			handler:function(but,e){
				me.AddSave(me.ProjectID);
			}
	    },'add','save');
		return buttonToolbarItems;
	},
	
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{text: '主键ID',dataIndex: 'PProjectDocument_Id',isKey: true,hidden: true,menuDisabled: false,sortable: false},
        {menuDisabled: false, text: '文档名称', dataIndex: 'PProjectDocument_DocumentName',	sortable: false,
		width: 240,editor: {xtype: 'textfield',allowBlank: false,name:'qqq' }//, defaultRenderer: true
        },
        {
			text: '模板内容',dataIndex: 'PProjectDocument_DocumentLink',width: 180,sortable: false, 
			width: 90,menuDisabled: false,
			renderer: function(value, meta, record) {
				var v='';
				if(record.get('PProjectDocument_DocumentLink')){
				    v='超文本连接';
				}
				return "<a style='color:#1A1AE6' onclick='showProjectDocument(this,event)' data='" + record.get('PProjectDocument_DocumentLink') + "'>"+ v +'</a>';
			}
		},{
			xtype: 'actioncolumn',text: '项目文档',align: 'center',width: 70,
			menuDisabled: false,sortable: false,style:'font-weight:bold;color:white;background:orange;',
			items: [{
				getClass: function(v, meta, record) {
					if(record.get('PProjectDocument_Id')){
						return 'button-edit hand';
					}else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var Id = rec.get(me.PKField);
		          	me.openEditForm(Id);
				}
			}] 
		},{
			text: '文档内容',dataIndex: 'PProjectDocument_Content',width: 180,sortable: false, 
			menuDisabled: false,
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta, record);
				return v;
			}
		}, {
			text: '显示次序',dataIndex: 'PProjectDocument_DispOrder',width: 70,sortable: false, 
			menuDisabled: false,editor: {
				xtype: 'numberfield',minValue: 0,allowBlank: false
			}
		}, {
				xtype: 'actioncolumn',text: '删除',align: 'center',width: 40,
				menuDisabled: false,sortable: false,style:'font-weight:bold;color:white;background:orange;',
				items: [{
					getClass: function(v, meta, record) {
						return 'button-del hand';
					},
					handler: function(grid, rowIndex, colIndex) {
						var rec = grid.getStore().getAt(rowIndex);
					    var Id = rec.get(me.PKField);
					    if(Id){
					    	me.onDelClick();
					    }else{
					    	me.store.remove(rec);
					    	if(me.store.getCount()==0){
					    		me.onSearch();
					    	}
					    }
					}
				}] 
		}];
        return columns;
    },
    onAddClick:function(){
    	var me=this;
    	var num=me.getStore().getCount()+1;
    	me.createAddRec(num);
    },
   	showMemoText:function(value, meta, record){
		var me=this	;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 	
		val = val.replace(/\\r\\n/g, "<br />");
        val = val.replace(/\\n/g, "<br />");
		var v ="" + value;
		var length=me.getStrLeng(value);
		if(length > 0)v = (length > 16 ? v.substring(0, 16) : v);
		if(length>16){
			v= v+"...";
		}
		v = v.replace(/<.*?>/ig,"");
		value = value.replace(me.regStr, "'");
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>文档内容:</b>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	},
    // UTF8字符集实际长度计算 
    getStrLeng:function (str){ 
	    var realLength = 0; 
	    var len = str.length; 
	    var charCode = -1; 
	    for(var i = 0; i < len; i++){ 
	        charCode = str.charCodeAt(i); 
	        if (charCode >= 0 && charCode <= 128) {  
	            realLength += 1; 
	        }else{  
	            // 如果是中文则长度加3 
	            realLength += 3; 
	        } 
	    }  
	    return realLength; 
	} ,
     /**选择帮助文档*/
	onOpenHelpForm: function(id,TaskHelp,rec) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.pproject.knowledgebase.CheckGrid', {
			PK: id,
			SUB_WIN_NO: '102',
			TaskHelp:TaskHelp,
			  /**是否单选*/
	        checkOne:true,
			listeners: {
				accept: function(p, record) {
				    var TaskHelp=record.get('FFile_Id');
					me.updateOneByTaskHelp(id,TaskHelp,p,rec);
				}
			}
		}).show();
	},
	updateOneByTaskHelp:function(id,TaskHelp,p,rec){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = {};
		params.entity = {Id:id};
		params.entity.DocumentLink = TaskHelp;
		params.fields = 'Id,DocumentLink';
		
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				if(data.success){
					rec.set('PProjectDocument_DocumentLink',TaskHelp);
					rec.commit();
					me.hideMask();//隐藏遮罩层
					p.close();
				}else{
					JShell.Msg.alert("保存失败");
				}
			});
		});
	},
   /**选择文档模板*/
	AddSave: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.pproject.knowledgebase.CheckGrid', {
			PK: id,
		    /**项目任务作业指导类型Id*/
			BDictTreeId: "5731661763405177841",
			IDS:'5133187353604336821',
			SUB_WIN_NO: '105',
			title:'文档模板选择',
			/**是否单选*/
        	checkOne:false,
			listeners: {
				accept: function(p, records) {
					p.showMask();
					me.onSave(p,records);
				}
			}
		}).show();
	},
	 /**增加一行*/
	createAddRec: function(num) {
		var me = this;
		var obj = {
			PProjectDocument_Id:'' ,
			PProjectDocument_DocumentName: '',
			PProjectDocument_DocumentLink:'',
			PProjectDocument_Content:'',
			PProjectDocument_DispOrder:num
		};
		me.store.insert(me.getStore().getCount(), obj);
	},
	
	/**打开修改(超链接)页面*/
	openAddForm:function(ProjectID){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.pproject.document.ContentForm', {
			SUB_WIN_NO:'107',//内部窗口编号
			resizable: false,
			formtype:'add',
			width:800,
			ProjectID:ProjectID,
//			PK:id,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**项目文档*/
	openEditForm:function(id){
		var me = this;
		var maxWidth = document.body.clientWidth - 360;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.wfm.business.pproject.document.AddPanel', {
			SUB_WIN_NO:'107',//内部窗口编号
			resizable: false,
			formtype:'edit',
			width:maxWidth,
			height:height,
			PK:id,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			params = [];
		//项目
		if(me.ProjectID) {
			params.push("pprojectdocument.PProject.Id=" + me.ProjectID + "");
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**根据id显示项目模板*/
	showFFileById: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.qms.file.basic.FFileDetailedForm', {
			//resizable: false,
			PK: id ,
			width:800,
			title:'文档内容',
			itemId: 'FFileOperationForm',
			FFileId : id
		}).show();
	},
	/**保存关系数据*/
	onSave:function(p,records){
		var me = this,
			ids = [],
			recs = [];
			
		if(records.length == 0) return;
			
//		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		
		//循环保存数据
		for(var i in records){
			var DispOrder=me.getStore().getCount()+ parseInt(i)+1;
			me.onAddOneLink(records[i], DispOrder,function(){
				p.close();
				me.onSearch();
			});
		}
	},
	/**新增关系数据*/
	onAddOneLink:function(record, DispOrder,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
	    var params = {};
		params.entity = {DocumentLink:record.get('FFile_Id'),
		    DocumentName:record.get('FFile_Title'),
		    DispOrder:DispOrder,
		    IsUse:1
	    };
	    var userId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		if(userId){
		    params.entity.Creater = {
				Id:userId,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
	    if(record.get('FFile_Memo')){
	    	 params.entity.Content=record.get('FFile_Memo');
	    }
		if(me.ProjectID){
			params.entity.PProject = {
				Id:me.ProjectID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		//提交数据到后台
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){callback();}
			}
		});
	},
	onSaveClick:function(){
		var me = this;
		var records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
		if(len == 0) return;
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			me.onSaveInfo(rec);
		}
	},
	onSaveInfo:function(rec){
		var me = this;
		var id = rec.get(me.PKField);
		var url=me.addUrl;
		if(id){
			url=me.editUrl;
		}
		url = JShell.System.Path.getRootUrl(url);
		if(!JShell.System.Cookie.map.USERID) {
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		var params = {};
		params.entity = {
			DocumentName:rec.get('PProjectDocument_DocumentName'),
		    DispOrder:rec.get('PProjectDocument_DispOrder'),
		    IsUse:1
		};
		if(me.ProjectID){
			params.entity.PProject = {
				Id:me.ProjectID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		var USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		
		if(USERID){
//			params.entity.CreatEmpID=USERID;
			params.entity.Creater = {
				Id:USERID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}

       
		if(id){
			params.entity.Id=id; 
			params.fields = 'Id,DocumentName,DispOrder';
		}
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				if(data.success){
					if(rec){
						rec.set('PProjectDocument_DocumentName',rec.get('PProjectDocument_DocumentName'));
						rec.set('PProjectDocument_DispOrder',rec.get('PProjectDocument_DispOrder'));
						rec.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(rec){
					rec.set('PProjectDocument_DocumentName',rec.get('PProjectDocument_DocumentName'));
						rec.set('PProjectDocument_DispOrder',rec.get('PProjectDocument_DispOrder'));
						rec.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) 
//					me.fireEvent('save',me);
					me.onSearch();
				}
			});
		},200);
	}
});