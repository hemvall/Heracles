import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import i18n from './plugins/i18n'

const app = createApp(App);
// dev
app.config.globalProperties.$api = "https://localhost:7000";
// prod
// app.config.globalProperties.$api = 'https://api.heracle.me'

app.use(router).use(i18n).mount("#app");
