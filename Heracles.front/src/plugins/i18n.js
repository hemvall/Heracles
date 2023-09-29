import { createI18n } from "vue-i18n";
import messages from "./lang/messages";

const i18n = createI18n({
  fallbackLocale: "fr",
  locale: localStorage.getItem("defaultLangCode") || 'fr',
  messages,
});

export default i18n;
