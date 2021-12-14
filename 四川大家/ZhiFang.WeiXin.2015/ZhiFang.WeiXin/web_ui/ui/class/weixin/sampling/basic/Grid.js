/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
 */
Ext.define('Shell.class.weixin.sampling.basic.Grid', {
    extend: 'Shell.ux.grid.Panel',
    title: '申请列表',
    //获取数据服务路径
    selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/GetNRequestFromListByByDetailsAndRBAC?isPlanish=true',
    //默认加载数据
    defaultLoad: false,
    //带功能按钮栏
    hasButtontoolbar: false,
    //排序字段
    defaultOrderBy: [{"property": "nrequestform_OperDate","direction": "asc"},{"property": "nrequestform_OperTime","direction": "asc"}],
    //带分页栏
    hasPagingtoolbar: true,
    //是否启用序号列
    hasRownumberer: true,
    //是否默认选中数据
    autoSelect: true,
    /**序号列宽度*/
    rowNumbererWidth: 35,
    //改变状态名称颜色
    initComponent: function () {
        var me = this;

        me.columns = [
            {
                text: '来源',
                dataIndex: 'ZDYZDY10',
				width:65,sort:true,align:'center',
				renderer:function (v, meta, record) { 
					var value = "" + record.get('NRequestForm_ZDY10');
					if(value != "" && value.length > 0) {
						meta.style="background-color:#FF9900;color:#FFFFFF;"; 						
						return "微信消费";
					} else {
						meta.style="background-color:#337ab7;color:#FFFFFF;"; 
						return "录 入";
					}					
					//tootip = "已经打印<b style='color:red;'> " + v + " </b>次",					
					//meta.tdAttr = 'data-qtip="' + tootip + '"';					
				}
            }, {
                text: '消费码',
                dataIndex: 'NRequestForm_ZDY10',
                width: 140
            }, {
                text: '条码号',
                dataIndex: 'NRequestForm_BarCodeNo',
                width: 170
            }, {
                text: '姓名',
                dataIndex: 'NRequestForm_CName',sort:true,
                width: 65
            }, {
                text: '性别',
                dataIndex: 'NRequestForm_GenderName',sort:true,
                width: 45,align: 'center'
            }, {
                text: '年龄(岁)',
                dataIndex: 'NRequestForm_Age',
                width: 60,align: 'center',
				renderer:function (v, meta, record) { 
					if(v=="200" || v == 200){
						return "成人";
					}else{
						return v;
					}			
				}
            }, {
                text: '样本',
                dataIndex: 'NRequestForm_SampleTypeName',sort:true,
                width: 60,align: 'center'
            }, {
                text: '项目',
                dataIndex: 'NRequestForm_ItemName',sort:true,
                width: 170,align: 'center'
            }, {
                text: '医生',
                dataIndex: 'NRequestForm_DoctorName',sort:true,
                width: 55,align: 'center'
            }, {
                text: '开单时间',
                dataIndex: 'NRequestForm_OperDate',sort:true,
                width: 135,align: 'center'
            }, {
                text: '采样时间',
                dataIndex: 'NRequestForm_CollectDate',sort:true,
                width: 135,align: 'center'
            }, {
                text: '采血站点',
                dataIndex: 'NRequestForm_WebLisSourceOrgName',sort:true,
                width: 140,align: 'center'
            }];
        me.callParent(arguments);
    },
    /**@overwrite 改变返回的数据*/
    changeResult: function(data) {
		//console.log(data);
    	return data;
    },
	/**创建数据字段*/
	getStoreFields: function(isString) {
		var me = this,
			columns = me._resouce_columns || [],
			length = columns.length,
			fields = [];
	
		for (var i = 0; i < length; i++) {
			if (columns[i].dataIndex) {
				if(columns[i].dataIndex != "ZDYZDY10"){
					var obj = isString ? columns[i].dataIndex : {
						name: columns[i].dataIndex,
						type: columns[i].type ? columns[i].type : 'string'
					};
					fields.push(obj);
				}
				
			}
		}
	
		return fields;
	},
});