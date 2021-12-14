/**
 * 区域列表
 * @author liangyl
 * @version 2017-03-21
 */
Ext.define('Shell.class.weixin.item.product.apply.AreaGrid', {
	extend: 'Shell.class.weixin.hospital.area.Grid',
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: true,
	width: 300,
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		//		var result = {},
		//			list = [],
		//			arr = [],
		//			obj = {};
		//		//添加全部行
		//	    obj={ClientEleArea_AreaCName:'全部',ClientEleArea_ShortName:'',
		//			ClientEleArea_ClientNo:'',ClientEleArea_Id:''
		//		};
		//		if(data.value){
		//			list=data.value.list;
		//			list.splice(0,0,obj);
		//		}else{
		//			list=[];
		//			list.push(obj);
		//		}
		//	    result.list =  data.value.list;
		return data;
	}
});