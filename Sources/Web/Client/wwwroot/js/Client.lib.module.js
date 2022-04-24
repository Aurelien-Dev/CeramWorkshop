
export async function beforeStart(options) {
    console.log("beforeStart");

}

export async function afterStarted() {
    console.log("afterStarted");

    new MainInit().init();

}