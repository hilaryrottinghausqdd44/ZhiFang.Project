/**
 * 微信选择列表
 * @author Jcall
 * @version 2017-02-14
 */
Ext.define('Shell.class.weixin.doctor.weixin.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    requires:['Ext.ux.RowExpander'],
    title:'微信选择列表',
    width:270,
    height:600,
    
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
//		me.defaultWhere = me.defaultWhere || '';
//		if(me.defaultWhere){
//			me.defaultWhere = '(' + me.defaultWhere + ') and ';
//		}
//		me.defaultWhere += 'bweixinaccount.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'昵称/手机号',isLike:true,
			fields:['bweixinaccount.UserName','bweixinaccount.MobileCode']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'昵称',dataIndex:'BWeiXinAccount_UserName',width:100,
			sortable:false,menuDisabled:true,renderer:me.showRenderer
		},{
			text:'手机号',dataIndex:'BWeiXinAccount_MobileCode',width:100,
			sortable:false,menuDisabled:true,renderer:me.showRenderer
		},{
			text:'微信头像',dataIndex:'BWeiXinAccount_HeadImgUrl',hidden:true,hideable:false
		},{
			text:'主键ID',dataIndex:'BWeiXinAccount_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	showRenderer:function(v,meta,record){
		var html = 
		"<div style='text-align:center;padding:5px;'>" +
			"<img style='width:64px;margin:5px;' alt='没有头像' src='" + record.get('BWeiXinAccount_HeadImgUrl') + "'/>" +
        	"<p><b>微信昵称</br></b> " + record.get('BWeiXinAccount_UserName') + "</p>" +
		"</div>";
		
		meta.tdAttr = 'data-qtip="' + html + '"';
		
		return v;
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if (isLike) {
				if(fields[i]=='bweixinaccount.MobileCode'){
					where.push(fields[i] + "='" + value + "'");
				}else{
					where.push(fields[i] + " like '%" + value + "%'");
				}
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	}
});