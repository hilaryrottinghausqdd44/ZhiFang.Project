/**
 * 服务器程序授权明细
 * @author longfc
 * @version 2016-10-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.basic.ProgramLicenceGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.DetailsGrid',
	
	title: '服务器程序授权明细',
	width: 800,
	height: 500,
	
	/**获取数据服务路径*/
	selectUrl: '',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerProgramLicenceByField',
	
	/**上传的授权申请文件的程序授权明细信息*/
	ApplyProgramInfoList: null,
	/**后台排序*/
	remoteSort: true,
	/**默认加载*/
	defaultLoad: false,
	isAllowEditing: true,
	/**是否启用序号列*/
	hasRownumberer: false,
	defaultOrderBy: [{
		property: 'SQH',
		direction: 'ASC'
	}, {
		property: 'LicenceTypeId',
		direction: 'ASC'
	}],
	features: [Ext.create("Ext.grid.feature.Grouping", {
		groupByText: "用本字段分组",
		showGroupsText: "显示分组",
		groupHeaderTpl: "{name}",
		startCollapsed: true
	})],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.isAllowEditing = me.isAllowEditing || true;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/*创建SQH列**/
	createSQHColumn: function() {
		var me = this;
		return {
			text: '授权程序&SQH',
			dataIndex: 'SQH',
			hidden: true,
			width: 150,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				value = me.sqhValuerenderer(value, meta, record, rowIndex, colIndex, store, view);
				var style = me.getCellStyle(record.get("LicenceStatusId"));
				if(style != "")
					meta.style = style;

				return value;
			},
			editor: {
				readOnly: false,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false) {
							com.setReadOnly(true);
						} else {
							me.comSetReadOnly(com, e);
						}
					}
				}
			}
		};
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push(me.createSQHColumn());
		columns.push({
			text: '变更类型',
			dataIndex: 'TYPE',
			width: 85,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var LicenceTypeId =  record.get('LicenceTypeId');
				var AHBeforeLicenceTypeId  = record.get('PreLicenceTypeId');
				var PreNodeTableCounts = record.get('PreNodeTableCounts');
	            if(!record.get('PreLicenceTypeId') && (PreNodeTableCounts && Number(PreNodeTableCounts)>0)){
	            	AHBeforeLicenceTypeId="1";
	            }
	            
				var statusName ="";
				var LicenceDate = record.get('LicenceDate');
				var strLicenceDate="";
                var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
				if(info && LicenceTypeId == info.Id) {
					strLicenceDate = '永久';
				}else{
					if(LicenceDate != "" && LicenceDate != null && LicenceDate != undefined) {
						strLicenceDate = Ext.util.Format.date(LicenceDate, 'Y-m-d');
					}
				}
				//时间是否相同
				var isIdentical ="";
				var PreLicenceDate  = "";
			    var PreNodeTableCounts = record.get('PreNodeTableCounts');
				
	            if(PreNodeTableCounts && Number(PreNodeTableCounts)==0)PreLicenceDate="";
	            if(!record.get('PreLicenceTypeId') ){
	            	if(PreNodeTableCounts && Number(PreNodeTableCounts)>0){
	            		PreLicenceDate = '永久';
	            	}
	            }else{
            		if(record.get("") != "" && record.get("PreLicenceDate") != null && record.get("PreLicenceDate") != undefined)
				        PreLicenceDate = Ext.util.Format.date(record.get("PreLicenceDate"), 'Y-m-d');
            	}
			    if(PreLicenceDate && PreLicenceDate != LicenceDate && LicenceDate){
			        isIdentical = '1';
			    }
            	if(PreLicenceDate && LicenceTypeId && LicenceTypeId!=AHBeforeLicenceTypeId){
            		statusName = "其他";
                	meta.style = 'background-color:#FFA500;color:#ffffff;';
            	}
            	//正常
            	if(PreLicenceDate && LicenceTypeId && LicenceTypeId==AHBeforeLicenceTypeId &&!isIdentical && strLicenceDate){
            		statusName = "正常";
            	}
            	//临时时间改变
                if(PreLicenceDate && LicenceTypeId =='2' && AHBeforeLicenceTypeId =='2' && isIdentical=='1'){
                	statusName = "临时时间改变";
                	meta.style = 'background-color:#CD00CD;color:#ffffff;';
                }
				if(PreLicenceDate && AHBeforeLicenceTypeId && LicenceTypeId && AHBeforeLicenceTypeId != LicenceTypeId){
	                //临时变商业
	                if(LicenceTypeId =='1' && AHBeforeLicenceTypeId =='2'){
	                	statusName = "临时变商业";
	                	meta.style = 'background-color:green;color:#ffffff;';
	                }
			    }
				return statusName;
			}
		});
		columns.push(me.createLicenceTypeIdColumn());
		columns.push({
			text: '',
			dataIndex: 'TYPE',
			width: 5,
			maxWidth :5,
			minWidth:5,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
                value = record.get('LicenceDate');
                var AHBeforeLicenceTypeId  = record.get('PreLicenceTypeId');
				var PreNodeTableCounts = record.get('PreNodeTableCounts');
	            if(!record.get('PreLicenceTypeId') && (PreNodeTableCounts && Number(PreNodeTableCounts)>0)){
	            	AHBeforeLicenceTypeId="1";
	            }
                var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
				if(info) {
					if(record.get('LicenceTypeId') == info.Id) {
						value = '永久';
					} else {
						if(value != "" && value != null && value != undefined) {
							value = Ext.util.Format.date(value, 'Y-m-d');
						}
					}
				}
                var AHBeforeDateTime  = "";
	            if(record.get('AHBeforeLicenceTypeId')=='1') {
            	    AHBeforeDateTime = '永久';
            	}else{
            		AHBeforeDateTime = Ext.util.Format.date(record.get("PreLicenceDate"), 'Y-m-d');
            	}
            	
            	if(AHBeforeDateTime && value && AHBeforeDateTime != value && record.get('LicenceTypeId')){
            		meta.style = 'background-color:#CD00CD';
			    }
				return '';
			}
		});
		columns.push(me.createLicenceDateColumn());
		columns.push(me.createLicenceStatusIdColumn());
		columns.push(me.createLicenceStatusNameColumn());
		columns.push({
			text: 'OA内有效状态',
			dataIndex: 'CalcDays',
			width: 85,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				CalcDays = Number(value) || 0;
                var BGColor = "",StatusName="正常";
	            var PreNodeTableCounts = record.get('PreNodeTableCounts');
	            if(PreNodeTableCounts && Number(PreNodeTableCounts)==0)StatusName="";

	            if(!record.get('PreLicenceTypeId') ){
	            	if(PreNodeTableCounts && Number(PreNodeTableCounts)>0){
	            		BGColor = "";
					    StatusName="正常";
	            	}
	            }else{
	            	if(CalcDays<0){
						BGColor = "#DCDCDC";
						StatusName="已过期";
					}
					if(CalcDays<=30 && CalcDays>0){
						BGColor = "#FFA500";
						StatusName="一个月内过期";
					}
					if(CalcDays<=7 && CalcDays>=0){
						BGColor = "red";
						StatusName="即将过期";
					}
	            }
	            
				if(BGColor != "")
					meta.style = 'background-color:' + BGColor + ';color:#ffffff';
				return  StatusName;
			}
		},{
			text: '授权程序',
			dataIndex: 'ProgramName',
			//width: 90,
			flex: 1,
			hidden: true,
			sortable: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);

				return value;
			}
		}, {
			text: '授权站点数',
			dataIndex: 'NodeTableCounts',
			width: 85,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				if(record.get("NodeTableCounts") != record.get("PreNodeTableCounts")) {
					meta.style = "background-color:red;color:#ffffff;";
				}
				return value;
			},
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('NodeTableCounts', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		});
		columns.push({
			text: 'ID',
			dataIndex: 'Id',
			hidden: true,
			isKey: true,
			hideable: false
		}, {
			text: '程序Id',
			dataIndex: 'ProgramID',
			hidden: true,
			hideable: false
		}, {
			text: '服务器授权ID',
			dataIndex: 'ServerLicenceID',
			hidden: true,
			hideable: false
		});
		columns.push({
			text: '上次授权站点数',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'PreNodeTableCounts',
			width: 110,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				if(record.get("NodeTableCounts") != record.get("PreNodeTableCounts")) {
					meta.style = "background-color:red;color:#ffffff;";
				}
				return value;
			}
		},{
			text: 'OA上次授权类型名称',
			dataIndex: 'PreLicenceTypeName',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 130,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var PreNodeTableCounts = record.get('PreNodeTableCounts');
	            if(!record.get('PreLicenceTypeId') && (PreNodeTableCounts && Number(PreNodeTableCounts)>0)){
	            	value="商业";
	            }
           
				return value;
			}
		},{
			text: 'OA内上次授权截止时间',
			dataIndex: 'PreLicenceDate',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 135,
			sortable: false,
			//isDate: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var PreNodeTableCounts = record.get('PreNodeTableCounts');
				
	            if(PreNodeTableCounts && Number(PreNodeTableCounts)==0)value="";

	            if(!record.get('PreLicenceTypeId') ){
	            	if(PreNodeTableCounts && Number(PreNodeTableCounts)>0){
	            		value = '永久';
	            	}
	            }else{
            		if(value != ""&&value != null && value != undefined)
				        value = Ext.util.Format.date(value, 'Y-m-d');
            	}
	            
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
//				if(value != ""&&value != null && value != undefined)
//					value = Ext.util.Format.date(value, 'Y-m-d');
				return value;
			}
		}, {
			text: 'OA内上次授权类型Id',
			dataIndex: 'PreLicenceTypeId',
			hidden: true,
			hideable: false
		});
		return columns;
	},
	showQtipValue: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;
		var SQH = record.get("SQH");
		var ProgramName = record.get("ProgramName");
		var qtipValue = "";
		qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>1.授权类型不是商业时,截止日期不能为空<br />2.当选择的截止日期不是星期三工作日时,系统会自动修改为星期三工作日<br />3.当授权站点数与上次的授权站点数不一致时,授权站点数与上次授权站点数列背景色为红色</b>" + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>SQH:</b>" + SQH + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>授权程序:</b>" + ProgramName + "</p>";
		if(qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	/**@public 根据where条件加载数据*/
	load: function() {
		var me = this,
			collapsed = me.getCollapsed();
		me.defaultLoad = true;
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}
		if(me.ApplyProgramInfoList != null) {
			me.store.loadData(me.ApplyProgramInfoList);
		} else {
			me.clearData();
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
	},
	sqhValuerenderer: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;
		var ProgramName = record.get("ProgramName");
		if(ProgramName != null && ProgramName != undefined && ProgramName != "")
			value = ProgramName + ",SQH为" + value;
		return value;
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			groupField: "SQH",
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if(data.success) {
						var info = data.value;
						if(info) {
							var type = Ext.typeOf(info);
							if(type == 'object') {
								info = info;
							} else if(type == 'array') {
								info.list = info;
								info.count = info.list.length;
							} else {
								info = {};
							}

							data.count = info.count || 0;
							data.list = info.list || [];
						} else {
							data.count = 0;
							data.list = [];
						}
						data = me.changeResult(data);
						me.fireEvent('changeResult', me, data);
					} else {
						me.errorInfo = data.msg;
					}
					response.responseText = Ext.JSON.encode(data);

					return response;
				}
			},
			listeners: {
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	}
});