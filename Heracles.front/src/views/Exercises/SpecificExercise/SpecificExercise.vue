<template>
    <a class="button" @click="formPerformanceOpen = !formPerformanceOpen">Add a performance</a>
    <div v-if="formPerformanceOpen">
        <p>Date</p>
        <input v-model="perfDate" type="date" />
        <p>Weight</p>
        <input v-model="Weight" type="number" />
        <p>Reps</p>
        <input v-model="Reps" type="number" />
        <p>Sets</p>
        <input v-model="Sets" type="number" />
        <p>Score</p>
        <input v-model="Score" type="number" />
        <button @click="createPerformance">Valider</button>
        <button @click="formExerciseOpen = !formExerciseOpen">Fermer</button>
    </div>
    <div class="typeBlock">
        <h1>{{ exercise.label }}</h1>
        <p v-for="p in performances" :key="p.id">{{ p.weight }}</p><br />

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
            exercise: {},
            performances: [],
            formPerformanceOpen: false,
            UserId: 1,
            perfDate: new Date().toISOString(),
            Weight: 0,
            Reps: 0,
            Sets: 0,
            Score: 0,
        };
    },
    methods: {
        fetchData() {
            this.exercise = {};
            this.performances = [];
            this.loading = true;

            fetch(`${this.$api}/exercises/${this.$route.params.exerciseId}`)
                .then(r => r.json())
                .then(json => {
                    this.exercise = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/performances`)
                .then(r => r.json())
                .then(json => {
                    this.performances = json;
                    this.loading = false;
                    return;
                });
        },
        createPerformance() {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    ExerciseId: this.$route.params.exerciseId,
                    UserId: this.UserId,
                    Date: this.perfDate,
                    Weight: this.Weight,
                    Reps: this.Reps,
                    Sets: this.Sets,
                    Score: this.Score
                })
            };
            fetch(`${this.$api}/performances`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("La performance a bien été ajoutée") }
                    else { alert("La performance a été ajoutée") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./SpecificExercise.scss');

template {
    margin: 0 10%;
}
</style>