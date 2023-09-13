import {createI18n} from 'vue-i18n'
import messages from './lang/messages'

const i18n = createI18n({
fallbackLocale: 'fr',
locale: 'en',
messages
})

export default i18n