<template>
    
    <a class="button">Add an exercise</a>
    <router-link :to="`/exercises/${label}`" v-for="e in exercises" :key="e.id" class="typeBlock">
        <h1>{{ e.label }}</h1>
        <!-- <div class="gallery">
            <div class="section" v-for="e in exercises" :key="e.id">
                <h1>{{ e.label }}</h1>
                <img src="../../assets/logo.png" />
            </div>
        </div> -->
    </router-link>
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
    },
    data() {
        return {
            exercises: [],
            types: [],
        };
    },
    methods: {
        fetchData() {
            this.exercises = [];
            this.types = [];
            this.loading = true;

            fetch(`${this.$api}/exercisetype`)
                .then(r => r.json())
                .then(json => {
                    this.types = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/exercises`)
                .then(r => r.json())
                .then(json => {
                    this.exercises = json;
                    this.loading = false;
                    return;
                });
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./ExercisesPage.scss');

template {
    margin: 0 10%;
}
</style>