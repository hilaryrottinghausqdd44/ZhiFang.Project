//依托于JcallShell.LocalStorage,JcallShell.JSON，
//需要先加载util/JcallShell文件

/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**本地数据存储_主页信息*/
JcallShell.LocalStorage.Home = {
    map: {
        'BUTTON_INDEX': 'HOME_000001'//当前定位功能按钮Index属性值
    },
    /**添加按钮*/
    setButtonIndex: function (index) {
        JcallShell.LocalStorage.set(this.map.BUTTON_INDEX, index);
    },
    /**获取用户列表*/
    getButtonIndex: function () {
        var index = JcallShell.LocalStorage.get(this.map.BUTTON_INDEX) || null;
        return index;
    }
};