//依托于JcallShell.LocalStorage,JcallShell.JSON，
//需要先加载util/JcallShell文件

/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**本地数据存储_订单信息*/
JcallShell.LocalStorage.UserOrder = {
    map: {
        'PAYMASK': 'USERORDER_MASK'//支付状态标记
    }
};