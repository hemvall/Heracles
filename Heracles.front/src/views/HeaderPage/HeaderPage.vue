<template>
    <div class="headerContainer">
        <div class="header">
            <img src="../../assets/logo.png" width="45px">
            <a href="/" class="logo">{{ $t('header.title') }}</a>

            <div style="position: absolute;right: 5%;top: 10%;" class="header-right">
                <!-- <a class="active" href="#home">{{ $t('header.button1') }}</a> -->
                <a @click="$i18n.locale = 'fr'"><img style="height: 45px;" src="../../assets/fr.svg" alt="" /></a>
                <a @click="$i18n.locale = 'en'"><img style="height: 45px;" src="../../assets/uk.svg" alt="" /></a>
                <!-- <a @click="location.reload();">Se déconnecter</a> -->
            </div>
        </div>
        <div class="sidenav">
            <a class="item" href="/"><img src="../../assets/Icon/home.svg">{{ $t('header.home') }}</a>
            <a class="item" href="/exercises"><img src="../../assets/Icon/dumbell.svg">{{ $t('header.exercises') }}</a>
            <a class="item"><img src="../../assets/Icon/goal.svg">{{ $t('header.goals') }}</a>
            <a class="item"><img src="../../assets/Icon/user.svg">{{ $t('header.profile') }}</a>
            <a class="item"><img src="../../assets/Icon/logout.svg" @click="clear">{{ $t('header.logout') }}</a>
        </div>

    </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
    },
    data() {
        return {
        };
    },
    methods: {
        fetchData() {
            this.loading = true;

        },
        clear() {
            console.log("aa")
            window.localStorage.clear()
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./HeaderPage.scss');

* {
    font-family: Arial, Helvetica, sans-serif;
}
</style>