/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
 */
Ext.define('Shell.class.weixin.sampling.basic.apply.unlock.Grid', {
    extend: 'Shell.ux.grid.Panel',
	requires:['Shell.ux.toolbar.Button'],
    title: '申请列表',
    //获取数据服务路径
    sUrl: '/ServerWCF/ZhiFangWeiXinService.svc/SearchUnConsumerUserOrderFormList',
	selectUrl: '',
	//取消消费采样服务
	UN_CONSUME_URL:JShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinService.svc/UnConsumerUserOrderForm",
    //默认加载数据
    defaultLoad: true,
    //带功能按钮栏
    hasButtontoolbar: true,
    //排序字段
    defaultOrderBy: [],
    //带分页栏
    hasPagingtoolbar: false,
    //是否启用序号列
    hasRownumberer: true,
    //是否默认选中数据
    autoSelect: false,
    /**序号列宽度*/
    rowNumbererWidth: 35,
	hasSearch:true,
	WeblisSourceOrgID:'',
	WeblisSourceOrgName:'',
	ConsumerAreaID:'',
    //改变状态名称颜色
    initComponent: function () {
        var me = this;
		me.sUrl += "?WeblisSourceOrgID="+me.WeblisSourceOrgID+"&WeblisSourceOrgName="+me.WeblisSourceOrgName+"&ConsumerAreaID="+me.ConsumerAreaID;
		me.selectUrl=me.sUrl;
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:false,
			fields:['SPayCode']};
        me.columns = [
            {
                text: '消费开始时间',
                dataIndex: 'ConsumerStartTime',
                width: 140
            }, {
                text: '医生姓名',
                dataIndex: 'DoctorName',
                width: 80,align: 'center'
            }, {
                text: '用户姓名',
                dataIndex: 'UserName',
                width: 80,align: 'center'
            }, {
                text: '消费码',
                dataIndex: 'PayCode',
                width: 120,align: 'center'
            }, {
                text: '用户订单编号',
                dataIndex: 'UOFCode',
                width: 120,align: 'center'
            }, {
                text: '消费单编号',
                dataIndex: 'OS_UserConsumerFormCode',
                width: 120,align: 'center'
            }, {
				menuDisabled: true,
                sortable: false,
                xtype: 'actioncolumn',
                text: '操作',width: 50,
                items: [{
	                xtype: 'button',
	                iconCls: 'button-uncheck',
	                tooltip: '解锁',
	                handler: function (grid, rowIndex, colIndex) {
	                	var rec = grid.getStore().getAt(rowIndex);
	                    me.onUnConsumeByPayCode(rec.data.PayCode);
	                }
	            }]
            }];
        me.callParent(arguments);
    },
	/**@overwrite 查询按钮点击处理方法*/
	onSearchClick: function(but, value) {
		var me = this;
		//查询栏为空时直接查询
		if (!value) {
			me.onSearch();
			return;
		}	
		me.selectUrl=me.sUrl+"&PayCode="+value;	
		me.onSearch();
	},
	//取消锁定
	onUnConsumeByPayCode:function(PayCode){
		var me = this;
		JcallShell.Msg.confirm({title:'提示',msg:"是否解锁选中数据?"},function(){
			me.onUnConsume(PayCode,function(success,msg){
				if(success){
					JShell.Msg.show("解锁成功！");
					me.onSearch();
				}else{
					JShell.Msg.error("解锁失败：" + msg);
				}
			});
		});
	},
	//取消锁定
	onUnConsume:function(payCode,callback){
		var me = this;
		me.showMask('消费码锁定取消中，请稍候……');
		var entity= {
			PayCode:payCode,
			WeblisSourceOrgID:me.WeblisSourceOrgID,
			WeblisSourceOrgName:me.WeblisSourceOrgName,
			ConsumerAreaID:me.ConsumerAreaID
		};
		var params = Ext.JSON.encode({jsonentity: entity});
		JShell.Server.post(me.UN_CONSUME_URL,params,function(data){
			if(data.success){
				me.hideMask();//取消遮罩层
				callback(true);
			}else{
				me.hideMask();//取消遮罩层
				callback(false,data.msg);		
			}
		});	
		
	}
});