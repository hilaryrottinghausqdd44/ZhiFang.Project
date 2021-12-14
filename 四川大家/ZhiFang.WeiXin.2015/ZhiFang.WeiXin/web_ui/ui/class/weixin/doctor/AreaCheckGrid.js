/**
 * 区域选择列表
 * @author liangyl	
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.doctor.AreaCheckGrid',{
    extend:'Shell.class.weixin.hospital.area.CheckGrid',
    title:'区域选择列表',
    width:320,
    height:350,
    /**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var result = {},
			list = [],
			arr = [],
			obj = {};
		//添加全部行
	    obj={ClientEleArea_AreaCName:'全部',ClientEleArea_ShortName:'',
			ClientEleArea_ClientNo:'',ClientEleArea_Id:''
		};
		if(data.value){
			list=data.value.list;
			list.splice(0,0,obj);
		}else{
			list=[];
			list.push(obj);
		}
	    result.list =  data.value.list;
		return result;
	}
 });