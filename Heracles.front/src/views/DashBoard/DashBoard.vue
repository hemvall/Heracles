<template>
        <div class="wholePage">
            <div class="sectionContainer">
                <div class="child left">
                    <a>Bienvenue <br> {{ user.name }}!</a>
                </div>
                <div class="child right">
                    <img style="height: 100px;" src="../../assets/crown.svg" />
                    <a>Vous ne disposez pas encore du premium <br> {{ user.name }}!</a>
                </div>
            </div>
            <div class="sectionContainer">
                <div class="child left">
                    <a>Bienvenue <br> {{ user.name }}!</a>
                </div>
                <div class="child right">
                    <a>Vous ne disposez pas encore du premium <br> {{ user.name }}!</a>
                </div>
            </div>
        </div>
        <br /><br />

</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
        this.idUser = localStorage.getItem('userId')
        console.log(this.userId)
    },
    data() {
        return {
            user: {},
            idUser: 0
        };
    },
    methods: {
        fetchData() {
            this.user = {};
            this.loading = true;

            fetch(`${this.$api}/users/${localStorage.getItem("userId")}`)
                .then(r => r.json())
                .then(json => {
                    this.user = json;
                    this.loading = false;
                    return;
                });
        },
        exercisesFromType(tId) {
            return this.exercises.filter(e => e.typeId == tId && e.userId == localStorage.getItem('userId'))
        }
    },
    computed: {
        typesFromUser() {
            return this.types.filter(e => e.userId == localStorage.getItem('userId'))
        },
    }
}
);
</script>

<style scoped lang="scss">
@import url('./DashBoard.scss');

template {
    margin: 0 10%;
}

.wholePage {
    margin-left: 22%;
    margin-top: 8%;
    color: white;
}

* {
    font-family: Arial, Helvetica, sans-serif;
}</style>