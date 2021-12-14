/**
 * 维护类型列表
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.emaintenancedata.StructGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '仪器模板列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ETemplet_DispOrder',
		direction: 'ASC'
	}],
	height: 500,
	width: 400,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**主键列*/
	PKField: 'ETempletEmp_Id',
	defaultWhere: '',
	/**模板id*/
	TempletID: '',
	/**默认每页数量*/
	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems = ['->', {
			iconCls: 'button-right',
			//			tooltip:'<b>收缩面板</b>',
			handler: function() {
				me.collapse();
			}
		}];
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '编号',dataIndex: 'ETemplet_Id',
			width: 150,
			hidden: true,
			isKey: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'tid',
			dataIndex: 'tid',
			width: 150,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'pid',
			dataIndex: 'pid',
			width: 150,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '质量记录类型',
			dataIndex: 'text',
			width: 160,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '项目',
			dataIndex: 'list',
			width: 150,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			hidden: true
		}, {
			text: '模板内容结构',
			dataIndex: 'ETemplet_TempletStruct',
			width: 150,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			hidden: true
		}];
		return columns;
	},
    /**
     * 字符长度计算
     *获得字符串实际长度，中文2，英文1
     *str要获得长度的字符串
     */
	getNum:function(str){
		if(!str) return;
		str = str.replace(/\s/ig,'');
	    var realLength = 0, len = str.length, charCode = -1;
	    for (var i = 0; i < len; i++) {
	      charCode = str.charCodeAt(i);
		  if (charCode >= 0 && charCode <= 128) 
		       realLength += 1;
		    else
		       realLength += 2;
	    }
	    return realLength/2;  
	},
	getData: function(operateData, text) {
		var me = this;
		var operateobj = {},
			OperateListData = [];
		var ItemDataType = 'C',
			ItemCode = '',
			MinValue = '',
			MaxValue = '',
			DataLength = '',
			DecimalLength = '',
			DefaultValue = '',
			ItemValueList = null,
			ItemHeight=null,
			ItemWidth=null,IsSpreadItemList=null,
			IsMultiSelect=null,IsInputItemValue=null;
		var strArr = [],
			textLength = 0;

		for(var i = 0; i < operateData.length; i++) {
			if(operateData[i].text || operateData[i].text.length>0){
				textLength=me.getNum(operateData[i].text);
				strArr.push(textLength);
			}
		}
		//项目text最大长度
		textLength = Math.max.apply(null, strArr);
		for(var j = 0; j < operateData.length; j++) {
			ItemDataType = 'C', ItemCode = '',
				MinValue = '', MaxValue = '', DataLength = '', DecimalLength = '',
				DefaultValue = '', ItemValueList = null,IsSpreadItemList=null,
				ItemHeight=null,ItemWidth=null,
				IsMultiSelect=null,IsInputItemValue=null,AddValue=null;
			//解析Para
        
			if(operateData[j].Para) {
				ItemDataType = operateData[j].Para.ItemDataType;
				DefaultValue = operateData[j].Para.DefaultValue;
				MaxValue = operateData[j].Para.MaxValue;
				MinValue = operateData[j].Para.MinValue;
				DataLength = operateData[j].Para.DataLength;
				DecimalLength = operateData[j].Para.DecimalLength;
				ItemValueList = operateData[j].Para.ItemValueList;
				IsSpreadItemList=operateData[j].Para.IsSpreadItemList;
				ItemHeight=operateData[j].Para.ItemHeight;
				ItemWidth=operateData[j].Para.ItemWidth;
				IsMultiSelect=operateData[j].Para.IsMultiSelect;
				IsInputItemValue=operateData[j].Para.IsInputItemValue;
				AddValue=operateData[j].Para.AddValue;
			}
			operateobj = {
				text: operateData[j].text,
				textLength:textLength,
				ItemDataType: ItemDataType,
				DefaultValue: DefaultValue,
				MaxValue: MaxValue,
				MinValue: MinValue,
				MaxDataLength: DataLength,
				DecimalLength: DecimalLength,
				ItemValueList: ItemValueList,
				ItemCode: operateData[j].tid,
				tid: operateData[j].tid,
				pid: operateData[j].pid,
				ptext: text,
				IsSpreadItemList:IsSpreadItemList,
				IsMultiSelect:IsMultiSelect,
				IsInputItemValue:IsInputItemValue,
				ItemWidth:ItemWidth,
				ItemHeight:ItemHeight,
				AddValue:AddValue
			}
			me.OperateListData.push(operateobj);
		}
      
		return me.OperateListData;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var result = {},
			list = [],
			arr = [],
			obj = {},
			OperateListData = [];
		var text = '';
		var data = data.value.list;
		var TempletStruct = '',
			value = '';
		var operateData = '';
		if(data[0].ETemplet_TempletStruct) {
			TempletStruct = Ext.JSON.decode(data[0].ETemplet_TempletStruct);
			value = TempletStruct.Tree;

			for(var i = 0; i < value.length; i++) {
				operateData = value[i].Tree;
				me.OperateListData = [];
				text = value[i].text;
				me.OperateListData = me.getData(operateData, text);
				obj = {
					text: value[i].text,
					tid: value[i].tid,
					pid: value[i].pid,
					list: Ext.encode(me.OperateListData)
				}
				arr.push(obj);
			}
		}
		result.list = arr;
		return result;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
		if(me.TempletID) {
			params.push("etemplet.Id=" + me.TempletID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	}
});