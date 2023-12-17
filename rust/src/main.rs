mod day1;
mod day2;
mod utils;

fn main() {
    run_day("Day1", day1::solve);
    run_day("Day2", day2::solve);
}

fn run_day(day: &str, f: fn()) {
    println!("{}", day);
    let now = std::time::Instant::now();

    f();

    println!("Took: {}ms", now.elapsed().as_millis());
}
