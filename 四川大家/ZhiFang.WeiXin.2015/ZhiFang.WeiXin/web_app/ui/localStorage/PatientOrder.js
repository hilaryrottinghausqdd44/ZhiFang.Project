//依托于JcallShell.LocalStorage,JcallShell.JSON，
//需要先加载util/JcallShell文件

/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**本地数据存储_自主下单信息*/
JcallShell.LocalStorage.PatientOrder = {
    map: {
        'PATIENT': 'PATIENTORDER_000001',//当前患者
        'PACKAGE_LIST':'PATIENTORDER_000002'//当前勾选的套餐
    },
    /**记录患者信息*/
    setPatient: function (value) {
    	var data = JcallShell.JSON.encode(value) || '';
        JcallShell.LocalStorage.set(this.map.PATIENT,data);
    },
    /**获取患者信息*/
    getPatient: function () {
        var data = JcallShell.LocalStorage.get(this.map.PATIENT);
        data = JcallShell.JSON.decode(data) || null;
        return data;
    },
    /**添加套餐*/
    addPackage:function(value){
    	var list = JcallShell.LocalStorage.get(this.map.PACKAGE_LIST),
			list = list ? JcallShell.JSON.decode(list) : [];
		
		var hasValue = false;
		for(var i in list){
			if(list[i].Id == value.Id){
				hasValue = true;
				break;
			}
		}
		
		//如果数据已存在，就返回true
		if(hasValue) return true;
		
		list.push(value);
		list = JcallShell.JSON.encode(list);

		JcallShell.LocalStorage.set(this.map.PACKAGE_LIST, list);
    },
    /**删除套餐*/
    removePackege:function(id){
    	var list = JcallShell.LocalStorage.get(this.map.PACKAGE_LIST),
			list = list ? JcallShell.JSON.decode(list) : [],
			len = list.length;

		for(var i = 0; i < len; i++) {
			if(list[i].Id == id) {
				list.splice(i, 1);
				break;
			}
		}
		
		list = JcallShell.JSON.encode(list);

		JcallShell.LocalStorage.set(this.map.PACKAGE_LIST, list);
    },
    /**获取套餐列表*/
    getPackageList:function(){
    	var list = JcallShell.LocalStorage.get(this.map.PACKAGE_LIST),
			list = list ? JcallShell.JSON.decode(list) : [];
			
		return list;
    },
    /**清空医嘱单信息*/
    removeAll:function(){
    	JcallShell.LocalStorage.set(this.map.PATIENT,'');
    	JcallShell.LocalStorage.set(this.map.PACKAGE_LIST,'');
    }
};